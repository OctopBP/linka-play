using Tobi.Letters.Extensions;
using Tobi.Letters.Infrastructure;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput: IInput
	{
		public ReactiveProperty<Vector2> mousePositionRx { get; } = new();
		public bool mouseButtonPressed => false;
		public void Update() => mousePositionRx.Value = UnityEngine.Input.mousePosition.XY();
	}
}