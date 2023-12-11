using Cysharp.Threading.Tasks;
using Extensions;
using Infrastructure;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Pool;

namespace Game.Conveyor
{
	public class ItemsFactory : IItemFactory<ItemOnConveyor>
	{
		private readonly IAssetProvider _assetProvider;

		private AssetReference _assetReference;
		private IObjectPool<ItemOnConveyor> _pool;
		
		public ItemsFactory(IAssetProvider assetProvider)
		{
			_assetProvider = assetProvider;
		}

		public async UniTask InitAsync(AssetReference assetReference)
		{
			_assetReference = assetReference;
			
			var go = await _assetProvider.Load<GameObject>(_assetReference);
			var prefab = go.GetComponent<ItemOnConveyor>();

			_pool = new ObjectPool<ItemOnConveyor>(
				createFunc: () => Object.Instantiate(prefab),
				actionOnGet: item => item.SetActive(),
				actionOnRelease: item => item.SetInactive(),
				defaultCapacity: 2
			);
		}

		public async UniTask<ItemOnConveyor> Create()
		{
			return _pool.Get();
			
			// Без пула
			// var prefab = await _assetProvider.Load<ItemOnConveyor>(_assetReference);
			// return Object.Instantiate(prefab);
		}
	}
}