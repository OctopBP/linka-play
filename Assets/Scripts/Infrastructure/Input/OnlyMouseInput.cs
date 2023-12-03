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
		public ReactiveProperty<Vector2> CursorPositionRx { get; } = new();
		public ReactiveProperty<bool> ClickButtonPressedRx { get; } = new();
		public ReactiveProperty<Option<float>> ClickProgressRx { get; } = new();
		public ReactiveProperty<Option<Bounds>> MaybeSelectedBounds { get; } = new();
		
		/// <summary> Hom much time we should look at one point to emit mouse click. </summary>
		private const float TimeToClick = 1.5f;
		/// <summary> Delay after click and before start trying catch new click. </summary>
		private const float ClickTimeout = 0.5f;
		/// <summary> Allowed movement that will not reset click emitting. </summary>
		private const float MoveThreshold = 10f;

		private readonly CompositeDisposable _disposables = new();

		private readonly Func<Vector2> _getMousePosition;
		private readonly EventSystem _eventSystem;
		
		public OnlyMouseInput(Func<Vector2> getMousePosition, EventSystem eventSystem)
		{
			_getMousePosition = getMousePosition;
			_eventSystem = eventSystem;
			
			ClickButtonPressedRx
				.WhereTrue()
				.Subscribe(_ => ClickButtonPressedRx.Value = false);

			Observable
				.EveryUpdate()
				.Subscribe(_ => Update());
		}

		private void Update()
		{
			var mousePos = _getMousePosition();
			var deltaMove = (CursorPositionRx.Value - mousePos).magnitude;
			if (deltaMove < MoveThreshold)
			{
				if (_disposables.IsEmpty())
				{
					Observable
						.Timer(TimeSpan.FromSeconds(ClickTimeout))
						.Subscribe(_ => StartProgress())
						.AddTo(_disposables);
				}
			}
			else
			{
				ResetButton();
			}
			
			CursorPositionRx.Value = mousePos;
		}

		private void StartProgress()
		{
			Observable.EveryUpdate()
				.Select(_ => Time.deltaTime)
				.Subscribe(dt => ClickProgressRx.Value = ClickProgressRx.Value.IfNone(0) + dt / TimeToClick)
				.AddTo(_disposables);

			Observable.Timer(TimeSpan.FromSeconds(TimeToClick))
				.Subscribe(_ => SetButtonPressed())
				.AddTo(_disposables);
		}

		private void SetButtonPressed()
		{
			ClickButtonPressedRx.Value = true;
			ClickProgressRx.Value = None;
			
			// EmulateClick();	
			ResetButton();
		}

		private void ResetButton()
		{
			ClickProgressRx.Value = None;
			_disposables.Clear();
		}

		private void EmulateClick()
		{
			// var screenPoint = RectTransformUtility.WorldToScreenPoint(mainCamera, cursorPositionRx.Value);
			var pointerEventData = new PointerEventData(_eventSystem) { position = CursorPositionRx.Value };

			var ray = Camera.main.ScreenPointToRay(CursorPositionRx.Value);
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