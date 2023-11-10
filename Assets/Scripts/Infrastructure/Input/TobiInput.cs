using Tobii.Gaming;
using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public class TobiInput : IInput
	{
		public ReactiveProperty<Vector2> mousePositionRx { get; } = new();
		public ReactiveProperty<bool> mouseButtonPressedRx { get; } = new();
		public void Update() => mousePositionRx.Value = TobiiAPI.GetGazePoint().Screen;
	}
}