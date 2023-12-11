using TMPro;
using UnityEngine;

namespace Game.Conveyor
{
	public class ItemOnConveyor : MonoBehaviour
	{
		[SerializeField] private TMP_Text text;
		
		public void SetItemValue(ItemValue itemValue) => text.SetText(itemValue.ToString());
	}
}