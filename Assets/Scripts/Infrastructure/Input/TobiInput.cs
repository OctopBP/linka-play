using Tobi.Letters.Infrastructure;
using Tobii.Gaming;
using UnityEngine;

namespace Tobi.Letters.Input
{
	public class TobiInput: IInput
	{
		public Vector2 mousePosition => TobiiAPI.GetGazePoint().Screen;
		public bool mouseButtonPressed => false;
	}
}