using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.Input;

namespace Game.Conveyor
{
    public class NextItemDeliveryState : IConveyorGameState
    {
        private readonly ConveyorGameStateMachine _stateMachine;
        private readonly IItemFactory<ItemView> _itemFactory;
        private readonly IInput _input;
        private readonly ILog _log;

        protected NextItemDeliveryState(
            ConveyorGameStateMachine stateMachine, IItemFactory<ItemView> itemFactory,
            IInput input, ILog log
        )
        {
            _stateMachine = stateMachine;
            _itemFactory = itemFactory;
            _input = input;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("Next Item Delivery State Enter()");
            
            _input.Enabled.Value = false;

            var item = _itemFactory.Create();

            // Need this for setup item state machine
            await UniTask.Yield();
            await item.Init();
            
            await _stateMachine.Enter<SelectBoxState>();
        }

        public UniTask Exit()
        {
            _input.Enabled.Value = true;
            return default;
        }
    }
}