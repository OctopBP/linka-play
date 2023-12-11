using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
    public interface IItemFactory<TAsset> where TAsset : class
    {
        void Init(AssetReference itemOnConveyorPrefab);
        UniTask<TAsset> Create();
    }
}