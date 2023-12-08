using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Infrastructure
{
    public interface IAssetProvider
    {
        public UniTask InitializeAsync();
        
        public UniTask<TAsset> Load<TAsset>(string key) where TAsset : class;
        public UniTask<TAsset> Load<TAsset>(AssetReference assetReference) where TAsset : class;
        
        public UniTask<List<string>> GetAssetsListByLabel(string label, Type type);
        public UniTask<TAsset[]> LoadAll<TAsset>(List<string> keys) where TAsset : class;

        public UniTask WarmupAssetByLabel(string label);
        
        public UniTask ReleaseAssetsByLabel(string label);
        public void ReleaseAssets(string assetKey);
        public void ReleaseAssets(AssetReference assetReference);

        public void Cleanup();
    }
}