using Extensions;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput : IInput
	{
		public ReactiveProperty<Vector2> mousePositionRx { get; } = new();
		public ReactiveProperty<bool> mouseButtonPressedRx { get; } = new();

		public void Update()
		{
			mousePositionRx.Value = UnityEngine.Input.mousePosition.XY();
			mouseButtonPressedRx.Value = UnityEngine.Input.GetMouseButtonDown(0);
		}
	}
}