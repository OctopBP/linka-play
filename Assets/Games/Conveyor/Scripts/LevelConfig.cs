using UnityEngine;

namespace Game.Conveyor
{
    [CreateAssetMenu(fileName = "ConveyorLevelSettings", menuName = "Games/Conveyor/Level Config")]
    public partial class LevelConfig : ScriptableObject
    {
        [SerializeField, PublicAccessor] private int streakToWin;
        [SerializeField, PublicAccessor] private ItemValue rightItem, leftItem;
    }
}
