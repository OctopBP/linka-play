using System;
using Extensions;
using Infrastructure.CursorService;
using LanguageExt;
using ModestTree;
using UniRx;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Infrastructure.Input
{
	public class OnlyMouseInput : IInput
	{
		public ReactiveProperty<Vector2> CursorPositionRx { get; } = new();
		public ReactiveProperty<bool> ClickButtonPressedRx { get; } = new();
		public ReactiveProperty<Option<float>> ClickProgressRx { get; } = new();
		public ReactiveProperty<Option<Bounds>> MaybeSelectedBounds { get; } = new();
		public ReactiveProperty<bool> Enabled { get; } = new();
		
		/// <summary> Hom much time we should look at one point to emit mouse click. </summary>
		private const float TimeToClick = 1.5f;
		
		/// <summary> Delay after click and before start trying catch new click. </summary>
		private const float ClickTimeout = 0.5f;
		
		/// <summary> Allowed movement that will not reset click emitting. </summary>
		private const float MoveThreshold = 10f;

		private readonly ICameraService _cameraService;
		private readonly IRaycastService _raycastService;

		private readonly ReactiveProperty<float> _deltaMoveRx = new();
		private readonly CompositeDisposable _progressDisposables = new();

		public delegate Vector2 GetMousePosition();
		
		private readonly GetMousePosition _getMousePosition;

		public OnlyMouseInput(
			ICameraService cameraService, IRaycastService raycastService, GetMousePosition getMousePosition
		)
		{
			_getMousePosition = getMousePosition;
			_cameraService = cameraService;
			_raycastService = raycastService;

			ClickButtonPressedRx
				.WhereTrue()
				.Subscribe(_ => ClickButtonPressedRx.Value = false);

			Observable
				.EveryUpdate()
				.Where(_ => Enabled.Value)
				.Subscribe(_ => Update());

			_deltaMoveRx
				.Where(deltaMove => deltaMove > MoveThreshold)
				.Subscribe(_ => ResetButton());
		}

		private void Update()
		{
			var mousePos = _getMousePosition();
			
			_deltaMoveRx.Value = (CursorPositionRx.Value - mousePos).magnitude;
			CursorPositionRx.Value = mousePos;
			
			var maybeCursorSelectable = _raycastService
				.RaycastFromCameraToPosition<ICursorSelectable>(_cameraService.MainCamera, mousePos);

			maybeCursorSelectable.Match(
				Some: selectable =>
				{
					TryToStartProgressTimer(selectable);
					MaybeSelectedBounds.Value = selectable.MaybeBounds;
				},
				None: () =>
				{
					ResetButton();
					MaybeSelectedBounds.Value = None;
				}
			);	
		}

		private void TryToStartProgressTimer(ICursorSelectable selectable)
		{
			if (_progressDisposables.IsEmpty())
			{
				Observable
					.Timer(TimeSpan.FromSeconds(ClickTimeout))
					.Subscribe(_ => StartProgress(selectable))
					.AddTo(_progressDisposables);
			}
		}

		private void StartProgress(ICursorSelectable selectable)
		{
			Observable.EveryUpdate()
				.Select(_ => Time.deltaTime)
				.Subscribe(dt => ClickProgressRx.Value = ClickProgressRx.Value.IfNone(0) + dt / TimeToClick)
				.AddTo(_progressDisposables);

			Observable.Timer(TimeSpan.FromSeconds(TimeToClick))
				.Subscribe(_ => SetButtonPressed(selectable))
				.AddTo(_progressDisposables);
		}

		private void SetButtonPressed(ICursorSelectable selectable)
		{
			ClickButtonPressedRx.Value = true;
			ClickProgressRx.Value = None;
			
			selectable.OnSelect();
			
			ResetButton();
		}

		private void ResetButton()
		{
			ClickProgressRx.Value = None;
			_progressDisposables.Clear();
		}
	}
}