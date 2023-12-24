using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
	[Serializable]
	public partial class ItemValue
	{
		[SerializeField] private AssetReferenceSprite emojiSprite;

		public string SpriteName => emojiSprite.SubObjectName;
	}
}