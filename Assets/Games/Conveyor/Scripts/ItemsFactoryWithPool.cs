using System;
using Cysharp.Threading.Tasks;
using Extensions;
using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;
using Zenject;

namespace Game.Conveyor
{
	public class ItemsFactoryWithPool<TItem> : IItemFactory<TItem> where TItem : MonoBehaviour
	{
		private readonly IAssetProvider _assetProvider;
		private readonly GameAssets _gameAssets;
		private readonly DiContainer _diContainer;
		
		private IObjectPool<TItem> _pool;
		
		public ItemsFactoryWithPool(DiContainer diContainer, IAssetProvider assetProvider, GameAssets gameAssets)
		{
			_diContainer = diContainer;
			_assetProvider = assetProvider;
			_gameAssets = gameAssets;
		}

		public async UniTask InitAsync(Func<GameAssets, AssetReference> extractAsset)
		{
			var go = await _assetProvider.Load<GameObject>(extractAsset(_gameAssets));
			var prefab = go.GetComponent<TItem>();

			_pool = new ObjectPool<TItem>(
				createFunc: () => _diContainer.InstantiatePrefabForComponent<TItem>(prefab),
				actionOnGet: item => item.SetActive(),
				actionOnRelease: item => item.SetInactive(),
				defaultCapacity: 2
			);
		}

		public async UniTask<TItem> Create() => _pool.Get();
	}
}