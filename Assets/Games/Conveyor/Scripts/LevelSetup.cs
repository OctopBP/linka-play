using UnityEngine;

namespace Game.Conveyor
{
    [CreateAssetMenu(fileName = "ConveyorLevelSettings", menuName = "Games/Conveyor/Level Setup")]
    public partial class LevelSetup : ScriptableObject
    {
        [SerializeField, PublicAccessor] private int streakToWin;
        [SerializeField, PublicAccessor] private ItemValue rightItem, leftItem;
    }
}
