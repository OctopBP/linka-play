using Tobi.Letters.Infrastructure;
using Tobii.Gaming;
using UniRx;
using UnityEngine;

namespace Tobi.Letters.Input
{
	public class TobiInput: IInput
	{
		public ReactiveProperty<Vector2> mousePositionRx { get; } = new();
		public bool mouseButtonPressed => false;
		public void Update() => mousePositionRx.Value = TobiiAPI.GetGazePoint().Screen;
	}
}