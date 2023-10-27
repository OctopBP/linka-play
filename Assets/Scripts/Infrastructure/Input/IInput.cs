using UniRx;
using UnityEngine;

namespace Infrastructure.Input
{
	public interface IInput
	{
		ReactiveProperty<Vector2> mousePositionRx { get; }
		bool mouseButtonPressed { get; }
		void Update();
	}
}