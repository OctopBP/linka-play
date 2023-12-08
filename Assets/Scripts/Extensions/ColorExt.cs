using UnityEngine;

namespace Extensions
{
	public static class ColorExt
	{
		public static Color WithAlpha(this Color self, float alpha) =>
			new(self.r, self.g, self.b, alpha);
	}
}