using UnityEngine;

namespace Game.Conveyor
{
    [CreateAssetMenu(fileName = "ConveyorLevelSettings", menuName = "Games/Conveyor/Level Setup")]
    public partial class LevelSetup : ScriptableObject
    {
        [SerializeField, PublicAccessor] int streakToWin;
        [SerializeField, PublicAccessor] ItemValue rightItem, leftItem;
    }
}
