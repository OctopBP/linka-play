using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public interface IInput
	{
		ReactiveProperty<Vector2> mousePositionRx { get; }
		ReactiveProperty<bool> mouseButtonPressedRx { get; }
		void Update();
	}
}