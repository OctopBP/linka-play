using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure;
using Infrastructure.Input;

namespace Game.Conveyor
{
    public class NextItemDeliveryState : IConveyorGameState
    {
        private readonly ConveyorGameStateMachine _stateMachine;
        private readonly IItemFactory<ItemOnConveyor> _itemFactory;
        private readonly ConveyorPath _conveyorPath;
        private readonly IInput _input;
        private readonly ILog _log;

        protected NextItemDeliveryState(
            ConveyorGameStateMachine stateMachine, IItemFactory<ItemOnConveyor> itemFactory,
            ConveyorPath conveyorPath, IInput input, ILog log
        )
        {
            _stateMachine = stateMachine;
            _itemFactory = itemFactory;
            _conveyorPath = conveyorPath;
            _input = input;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("Next Item Delivery State Enter()");
            
            _input.Enabled.Value = false;
            
            var newItem = await _itemFactory.Create();
            newItem.transform.position = _conveyorPath.SpawnPoint;
            await newItem.transform.DOMove(_conveyorPath.StopPoint, 3f);
            
            await _stateMachine.Enter<SelectBoxState>();
        }

        public UniTask Exit()
        {
            _input.Enabled.Value = true;
            return default;
        }
    }
}