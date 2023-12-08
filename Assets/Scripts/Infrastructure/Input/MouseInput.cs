using Extensions;

namespace Infrastructure.Input
{
	public class MouseInput : OnlyMouseInput
	{
		public MouseInput(ICameraService cameraService, IRaycastService raycastService) :
			base(cameraService, raycastService, getMousePosition: () => UnityEngine.Input.mousePosition.XY()) { }
	}
}