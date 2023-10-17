using Tobi.Letters.Extensions;
using Tobi.Letters.Infrastructure;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput: IInput
	{
		public ReactiveProperty<Vector2> mousePosition { get; } = new();
		public bool mouseButtonPressed => false;
		public void Update() => mousePosition.Value = UnityEngine.Input.mousePosition.XY();
	}
}