using UnityEngine;

namespace Tobi.Letters.Extensions
{
	public static class VectorExt
	{
		public static Vector2 xy(this Vector3 self) => new(self.x, self.y);
	}
}