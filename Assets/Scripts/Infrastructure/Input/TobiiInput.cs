using Tobii.Gaming;
using UnityEngine.EventSystems;

namespace Infrastructure.Input
{
	public class TobiiInput : OnlyMouseInput
	{
		public TobiiInput(EventSystem eventSystem) : base(
			getMousePosition: () => TobiiAPI.GetGazePoint().Screen, 
			eventSystem: eventSystem
		) { }
	}
}