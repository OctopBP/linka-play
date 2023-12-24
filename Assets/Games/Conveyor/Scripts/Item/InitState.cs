using Cysharp.Threading.Tasks;

namespace Game.Conveyor
{
    public class InitState : IItemState
    {
        private readonly ItemStateMachine _itemStateMachine;
        private readonly ItemView _itemView;
        private readonly LevelConfigProvider _levelConfigProvider;
        private readonly ConveyorPath _conveyorPath;

        public InitState(
            ItemStateMachine itemStateMachine, ItemView itemView, LevelConfigProvider levelConfigProvider,
            ConveyorPath conveyorPath
        )
        {
            _itemStateMachine = itemStateMachine;
            _itemView = itemView;
            _levelConfigProvider = levelConfigProvider;
            _conveyorPath = conveyorPath;
        }

        public async UniTask Enter()
        {
            var itemValue = _levelConfigProvider.GetRandomItemValue();
            _itemView.SetValue(itemValue);
            _itemView.transform.position = _conveyorPath.SpawnPoint;
            
            await _itemStateMachine.Enter<MoveToStopPointState>();
        }

        public UniTask Exit() => default;
    }
}