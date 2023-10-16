using Tobi.Letters.Extensions;
using Tobi.Letters.Infrastructure;
using UnityEngine;

namespace Infrastructure.Input
{
	public class KeyboardInput: IInput
	{
		public Vector2 mousePosition => UnityEngine.Input.mousePosition.xy();
		public bool mouseButtonPressed => false;
	}
}