using System;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
    public interface IItemFactory<TAsset> where TAsset : class
    {
        UniTask InitAsync(Func<GameAssets, AssetReference> extractAsset);
        TAsset Create();
        void Release(TAsset asset);
    }
}