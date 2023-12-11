using Cysharp.Threading.Tasks;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

namespace Game.Conveyor
{
    public class ConveyorBootstrap : MonoBehaviour, IGameBootstrap
    {
        [SerializeField] private AssetReference itemOnConveyorPrefab;
        
        private ConveyorGameStateMachine _conveyorGameStateMachine;
        private IStateFactory _stateFactory;
        private IItemFactory<ItemOnConveyor> _itemFactory;

        [Inject]
        private void Construct(
            ConveyorGameStateMachine conveyorGameStateMachine, IStateFactory stateFactory,
            IItemFactory<ItemOnConveyor> itemFactory
        )
        {
            _stateFactory = stateFactory;
            _conveyorGameStateMachine = conveyorGameStateMachine;
            _itemFactory = itemFactory;
        }

        private void Start()
        {
            SetupStateMachine();
            _itemFactory.Init(itemOnConveyorPrefab);
        }

        private void SetupStateMachine()
        {
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<NextItemDeliveryState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<SelectBoxState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<WinState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<LoseState>());

            _conveyorGameStateMachine.Enter<BootstrapState>().Forget();
        }
    }
}