using Extensions;
using UnityEngine.EventSystems;

namespace Infrastructure.Input
{
	public class MouseInput : OnlyMouseInput
	{
		public MouseInput(EventSystem eventSystem) : base(
			getMousePosition: () => UnityEngine.Input.mousePosition.XY(), 
			eventSystem: eventSystem
		) { }
	}
}