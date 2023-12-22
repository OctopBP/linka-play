using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Game.Conveyor
{
	public class ItemsFactoryWithPool<TItem> : IItemFactory<TItem> where TItem : MonoBehaviour
	{
		private readonly IAssetProvider _assetProvider;
		private readonly GameAssets _gameAssets;
		
		private IObjectPool<TItem> _pool;
		
		public ItemsFactoryWithPool(IAssetProvider assetProvider, GameAssets gameAssets)
		{
			_assetProvider = assetProvider;
			_gameAssets = gameAssets;
		}

		public async UniTask InitAsync(Func<GameAssets, AssetReference> extractAsset)
		{
			var go = await _assetProvider.Load<GameObject>(extractAsset(_gameAssets));
			var prefab = go.GetComponent<TItem>();

			_pool = new ObjectPool<TItem>(
				createFunc: () => Object.Instantiate(prefab),
				actionOnGet: item => item.SetActive(),
				actionOnRelease: item => item.SetInactive(),
				defaultCapacity: 2
			);
		}

		public async UniTask<TItem> Create() => _pool.Get();
	}
}