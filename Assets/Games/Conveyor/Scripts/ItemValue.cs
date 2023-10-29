using System;
using UnityEngine;

namespace Game.Conveyor
{
	[Serializable]
	public partial class ItemValue
	{
		[SerializeField, PublicAccessor] char value;
	}
}