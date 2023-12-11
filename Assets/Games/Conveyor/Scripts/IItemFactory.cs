using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
    public interface IItemFactory<TAsset> where TAsset : class
    {
        UniTask InitAsync(AssetReference assetReference);
        UniTask<TAsset> Create();
    }
}