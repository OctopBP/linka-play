using UniRx;
using UnityEngine;

namespace Tobi.Letters.Infrastructure
{
	public interface IInput
	{
		ReactiveProperty<Vector2> mousePositionRx { get; }
		bool mouseButtonPressed { get; }
		void Update();
	}
}