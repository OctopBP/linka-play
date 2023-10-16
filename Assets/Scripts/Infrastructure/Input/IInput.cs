using UnityEngine;

namespace Tobi.Letters.Infrastructure
{
	public interface IInput
	{
		Vector2 mousePosition { get; }
		bool mouseButtonPressed { get; }
	}
}