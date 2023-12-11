using TMPro;
using UnityEngine;

namespace Game.Conveyor
{
	public class ItemOnConveyor : MonoBehaviour
	{
		[SerializeField] private TMP_Text text;

		public void SetText(ItemValue itemValue) => text.SetText(itemValue.Value.ToString());
	}
}