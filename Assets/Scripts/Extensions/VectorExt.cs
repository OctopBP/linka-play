using UnityEngine;

namespace Extensions
{
	public static class VectorExt
	{
		public static Vector2 XY(this Vector3 self) => new(self.x, self.y);
		
		public static Vector3 WithZ(this Vector2 self, float z) => new(self.x, self.y, z);
		
		public static Vector3 WithX(this Vector3 self, float x) => new(x, self.y, self.z);
		public static Vector3 WithY(this Vector3 self, float y) => new(self.x, y, self.z);
		public static Vector3 WithZ(this Vector3 self, float z) => new(self.x, self.y, z);
	}
}