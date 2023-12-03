using Extensions;
using LanguageExt;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput : IInput
	{
		public ReactiveProperty<Vector2> CursorPositionRx { get; } = new();
		public ReactiveProperty<bool> ClickButtonPressedRx { get; } = new();
		public ReactiveProperty<Option<float>> ClickProgressRx { get; } = new();
		public ReactiveProperty<Option<Bounds>> MaybeSelectedBounds { get; } = new();

		public KeyboardInput()
		{
			Observable
				.EveryUpdate()
				.Subscribe(_ => Update());
		}

		private void Update()
		{
			CursorPositionRx.Value = UnityEngine.Input.mousePosition.XY();
			ClickButtonPressedRx.Value = UnityEngine.Input.GetMouseButtonDown(0);
		}
	}
}