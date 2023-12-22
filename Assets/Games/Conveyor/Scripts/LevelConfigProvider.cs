using Infrastructure;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
    public partial class LevelConfigProvider : MonoBehaviour
    {
        [SerializeField, PublicAccessor] private LevelConfig levelConfig;

        private readonly IRnd _rnd;

        [Inject]
        public LevelConfigProvider(IRnd rnd)
        {
            _rnd = rnd;
        }

        public ItemValue GetRandomItemValue() => _rnd.NextBool() ? levelConfig.LeftItem : levelConfig.RightItem;
    }
}