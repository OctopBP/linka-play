using Extensions;
using LanguageExt;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput : IInput
	{
		public ReactiveProperty<Vector2> cursorPositionRx { get; } = new();
		public ReactiveProperty<bool> clickButtonPressedRx { get; } = new();

		public ReactiveProperty<Option<float>> clickProgressRx { get; } = new();

		public KeyboardInput()
		{
			Observable
				.EveryUpdate()
				.Subscribe(_ => Update());
		}
		
		void Update()
		{
			cursorPositionRx.Value = UnityEngine.Input.mousePosition.XY();
			clickButtonPressedRx.Value = UnityEngine.Input.GetMouseButtonDown(0);
		}
	}
}