using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
	[Serializable]
	public partial class ItemValue
	{
		[SerializeField, PublicAccessor] private AssetReferenceSprite emojiSprite;
	}
}