using UnityEngine;

namespace Extensions
{
	public static class ComponentExt
	{
		/// <summary>
		/// Call <see cref="GameObject.SetActive"/> on component gameObject with given value.
		/// </summary>
		public static void SetActive(this Component self, bool value) => self.gameObject.SetActive(value);
		
		/// <summary>
		/// Call <see cref="GameObject.SetActive"/> on component gameObject with true value.
		/// </summary>
		public static void SetActive(this Component self) => self.gameObject.SetActive(true);
		
		/// <summary>
		/// Call <see cref="GameObject.SetActive"/> on component gameObject with false value.
		/// </summary>
		public static void SetInactive(this Component self) => self.gameObject.SetActive(false);
		
		/// <summary>
		/// Call <see cref="GameObject.SetActive"/> on gameObject with true value.
		/// </summary>
		public static void SetActive(this GameObject self) => self.SetActive(true);
		
		/// <summary>
		/// Call <see cref="GameObject.SetActive"/> on gameObject with false value.
		/// </summary>
		public static void SetInactive(this GameObject self) => self.SetActive(false);
	}
}