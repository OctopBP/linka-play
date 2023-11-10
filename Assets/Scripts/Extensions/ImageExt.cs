using UnityEngine.UI;

namespace Extensions
{
	public static class ImageExt
	{
		public static void SetAlpha(this Image self, float alpha) =>
			self.color = new(self.color.r, self.color.g, self.color.b, alpha);
	}
}