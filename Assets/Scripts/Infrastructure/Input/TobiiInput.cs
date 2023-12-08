using Tobii.Gaming;

namespace Infrastructure.Input
{
	public class TobiiInput : OnlyMouseInput
	{
		public TobiiInput(ICameraService cameraService, IRaycastService raycastService) :
			base(cameraService, raycastService, getMousePosition: () => TobiiAPI.GetGazePoint().Screen) { }
	}
}