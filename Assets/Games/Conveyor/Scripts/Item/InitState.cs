using Cysharp.Threading.Tasks;
using Infrastructure;

namespace Game.Conveyor
{
    public class InitState : IItemState
    {
        private readonly ItemStateMachine _itemStateMachine;
        private readonly ItemView _itemView;
        private readonly LevelConfigProvider _levelConfigProvider;
        private readonly ConveyorPath _conveyorPath;
        private readonly ILog _log;

        public InitState(
            ItemStateMachine itemStateMachine, ItemView itemView, LevelConfigProvider levelConfigProvider,
            ConveyorPath conveyorPath, ILog log
        )
        {
            _itemStateMachine = itemStateMachine;
            _itemView = itemView;
            _levelConfigProvider = levelConfigProvider;
            _conveyorPath = conveyorPath;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("InitState Enter()", LogTag.Item);
            
            _itemView.transform.position = _conveyorPath.SpawnPoint;
            
            var itemValue = _levelConfigProvider.GetRandomItemValue();
            await _itemView.SetValue(itemValue);

            await _itemStateMachine.Enter<MoveToStopPointState>();
        }

        public UniTask Exit() => default;
    }
}