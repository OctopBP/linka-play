using System;
using Extensions;
using LanguageExt;
using ModestTree;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static LanguageExt.Prelude;

namespace Infrastructure.Input
{
	public class OnlyMouseInput : IInput
	{
		public ReactiveProperty<Vector2> cursorPositionRx { get; } = new();
		public ReactiveProperty<bool> clickButtonPressedRx { get; } = new();
		public ReactiveProperty<Option<float>> clickProgressRx { get; } = new();
		
		/// <summary> Hom much time we should look at one point to emit mouse click. </summary>
		const float TimeToClick = 1.5f;
		/// <summary> Delay after click and before start trying catch new click. </summary>
		const float ClickTimeout = 0.5f;
		/// <summary> Allowed movement that will not reset click emitting. </summary>
		const float MoveThreshold = 10f;
		
		readonly CompositeDisposable disposables = new();

		readonly Func<Vector2> getMousePosition;
		readonly EventSystem eventSystem;
		
		public OnlyMouseInput(Func<Vector2> getMousePosition, EventSystem eventSystem)
		{
			this.getMousePosition = getMousePosition;
			this.eventSystem = eventSystem;
			
			clickButtonPressedRx
				.WhereTrue()
				.Subscribe(_ => clickButtonPressedRx.Value = false);

			Observable
				.EveryUpdate()
				.Subscribe(_ => Update());
		}
		
		void Update()
		{
			var mousePos = getMousePosition();
			var deltaMove = (cursorPositionRx.Value - mousePos).magnitude;
			if (deltaMove < MoveThreshold)
			{
				if (disposables.IsEmpty())
				{
					Observable
						.Timer(TimeSpan.FromSeconds(ClickTimeout))
						.Subscribe(_ => StartProgress())
						.AddTo(disposables);
				}
			}
			else
			{
				ResetButton();
			}
			
			cursorPositionRx.Value = mousePos;
		}

		void StartProgress()
		{
			Observable.EveryUpdate()
				.Select(_ => Time.deltaTime)
				.Subscribe(dt => clickProgressRx.Value = clickProgressRx.Value.IfNone(0) + dt / TimeToClick)
				.AddTo(disposables);

			Observable.Timer(TimeSpan.FromSeconds(TimeToClick))
				.Subscribe(_ => SetButtonPressed())
				.AddTo(disposables);
		}

		void SetButtonPressed()
		{
			clickButtonPressedRx.Value = true;
			clickProgressRx.Value = None;
			
			// EmulateClick();	
			ResetButton();
		}
	
		void ResetButton()
		{
			clickProgressRx.Value = None;
			disposables.Clear();
		}

		void EmulateClick()
		{
			// var screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, cursorPositionRx.Value);
			var pointerEventData = new PointerEventData(eventSystem) { position = cursorPositionRx.Value };

			var ray = Camera.main.ScreenPointToRay(cursorPositionRx.Value);
			if (!Physics.Raycast(ray, out var hit)) return;
			
			hit.transform.MaybeComponent<Button>()
				.IfSome(c => c.OnPointerClick(pointerEventData));
			
			// var results = new List<RaycastResult>();
			// _graphicRaycaster.Raycast(pointerEventData, results);
			// foreach (var result in results)
			// {
			// 	result.gameObject
			// 		.MaybeComponent<Button>()
			// 		.IfSome(c => c.OnPointerClick(pointerEventData));
			// }
		}
	}
}