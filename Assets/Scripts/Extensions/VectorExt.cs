using UnityEngine;

namespace Tobi.Letters.Extensions
{
	public static class VectorExt
	{
		public static Vector2 XY(this Vector3 self) => new(self.x, self.y);
		public static Vector3 WithZ(this Vector2 self, float z) => new(self.x, self.y, z);
	}
}