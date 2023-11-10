using TMPro;
using UnityEngine;

namespace Game.Conveyor
{
	public class ItemOnConveyor : MonoBehaviour
	{
		[SerializeField] TMP_Text text;

		public class Model
		{
			public Model(ItemOnConveyor backing, ItemValue itemValue)
			{
				backing.text.SetText(itemValue._value.ToString());
			}
		}
	}
}