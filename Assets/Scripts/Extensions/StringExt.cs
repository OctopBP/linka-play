using UnityEngine;

namespace Extensions
{
	public static class StringExt
	{
		/// <summary>
		/// Returns the syntax for Unity text handlers to color the <see cref="content"/>.
		/// <para/>
		/// See https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#supported-tags for more info.
		/// </summary>
		public static string WrapInColorTag(this string content, Color color) =>
			$"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{content}</color>";
  
		/// <summary>
		/// Returns the syntax for Unity text handlers to color the <see cref="content"/>.
		/// <para/>
		/// See https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#supported-tags for more info.
		/// </summary>
		public static string WrapInColorTag(this string content, string htmlColor) =>
			$"<color={htmlColor}>{content}</color>";
		
		/// <summary>
		/// Returns the syntax for Unity text handlers to bold the <see cref="content"/>.
		/// <para/>
		/// See https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#supported-tags for more info.
		/// </summary>
		public static string WrapInBoldTag(this string content) =>
			$"<b>{content}</b>";
		
		/// <summary>
		/// Returns the syntax for Unity text handlers to italic the <see cref="content"/>.
		/// <para/>
		/// See https://docs.unity3d.com/Packages/com.unity.ugui@1.0/manual/StyledText.html#supported-tags for more info.
		/// </summary>
		public static string WrapInItalicTag(this string content) =>
			$"<i>{content}</i>";
	}
}