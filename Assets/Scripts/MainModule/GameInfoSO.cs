using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Core
{
    [CreateAssetMenu(fileName = "GameInfo", menuName = "Games/Game Info")]
    public partial class GameInfoSO : ScriptableObject
    {
        [SerializeField, PublicAccessor] string displayName;
        [SerializeField, PublicAccessor] AssetReference scene;
    }
}
