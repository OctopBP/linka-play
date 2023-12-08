using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Infrastructure
{
    public class AssetProvider : IAssetProvider
    {
        private readonly Dictionary<string, AsyncOperationHandle> _assetRequests = new();

        public async UniTask InitializeAsync() => await Addressables.InitializeAsync().ToUniTask();

        public async UniTask<TAsset> Load<TAsset>(string key) where TAsset : class
        {
            if (!_assetRequests.TryGetValue(key, out var handle))
            {
                handle = Addressables.LoadAssetAsync<TAsset>(key);
                _assetRequests.Add(key, handle);
            }

            await handle.ToUniTask();

            return handle.Result as TAsset;
        }

        public async UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class =>
            await Load<TAsset>(assetReference.AssetGUID);

        public async UniTask<List<string>> GetAssetsListByLabel(string label, Type type = null)
        {
            var operationHandle = Addressables.LoadResourceLocationsAsync(label, type);
            var locations = await operationHandle.Task.AsUniTask();

            var assetKeys = new List<string>(locations.Count);
            foreach (var location in locations)
            {
                assetKeys.Add(location.PrimaryKey);
            }
            
            Addressables.Release(operationHandle);
            return assetKeys;
        }

        public async UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class
        {
            var tasks = new List<UniTask<TAsset>>(keys.Count);
            
            foreach (var key in keys)
            {
                tasks.Add(Load<TAsset>(key));
            }

            return await UniTask.WhenAll(tasks);
        }

        public async UniTask WarmupAssetByLabel(string label)
        {
            var assetsList = await GetAssetsListByLabel(label);
            await LoadAll<object>(assetsList);
        }
        
        public async UniTask ReleaseAssetsByLabel(string label)
        {
            var assetsList = await GetAssetsListByLabel(label);
            foreach (var assetKey in assetsList)
            {
                ReleaseAssets(assetKey);
            }
        }
        
        public void ReleaseAssets(string assetKey)
        {
            if (_assetRequests.TryGetValue(assetKey, out var handle))
            {
                Addressables.Release(handle);
                _assetRequests.Remove(assetKey);
            }
        }

        public void ReleaseAssets(AssetReference assetReference) =>
            ReleaseAssets(assetReference.AssetGUID);

        public void Cleanup()
        {
            foreach (var assetRequest in _assetRequests)
            {
                Addressables.Release(assetRequest.Value);
            }
            
            _assetRequests.Clear();
        }
    }
}