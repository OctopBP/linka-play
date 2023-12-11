using System;
using UnityEngine;

namespace Game.Conveyor
{
	[Serializable]
	public partial class ItemValue
	{
		[SerializeField, PublicAccessor] private char value;
		[SerializeField, PublicAccessor] private Emoji emoji;

		public override string ToString() => value.ToString();
	}
}