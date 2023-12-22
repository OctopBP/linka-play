using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Conveyor
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "Games/Conveyor/Game Assets")]
    public partial class GameAssets : ScriptableObject
    {
        [SerializeField, PublicAccessor] private AssetReference item;
        [SerializeField, PublicAccessor] private AssetReference bigBox;
    }
}