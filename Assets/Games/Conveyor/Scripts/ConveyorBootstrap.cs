using Cysharp.Threading.Tasks;
using Infrastructure.States;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
    public class ConveyorBootstrap : MonoBehaviour, IGameBootstrap
    {
        [SerializeField] private Transform leftBoxPoint, rightBoxPoint;
        
        private ConveyorGameStateMachine _conveyorGameStateMachine;
        private IStateFactory _stateFactory;
        private IItemFactory<ItemView> _itemFactory;
        private IItemFactory<BigBoxView> _boxFactory;
        private LevelConfigProvider _levelConfigProvider;

        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private void Construct(
            ConveyorGameStateMachine conveyorGameStateMachine, IStateFactory stateFactory,
            IItemFactory<ItemView> itemFactory, IItemFactory<BigBoxView> boxFactory,
            LevelConfigProvider levelConfigProvider
        )
        {
            _stateFactory = stateFactory;
            _conveyorGameStateMachine = conveyorGameStateMachine;
            _itemFactory = itemFactory;
            _boxFactory = boxFactory;
            _levelConfigProvider = levelConfigProvider;
        }

        private async void Start()
        {
            await _itemFactory.InitAsync(ga => ga.Item);
            await SetupBigBoxes();
            await SetupStateMachine();
        }

        private async UniTask SetupBigBoxes()
        {
            await _boxFactory.InitAsync(ga => ga.BigBox);

            var boxLeft = await _boxFactory.Create();
            boxLeft.transform.position = leftBoxPoint.position;
            await boxLeft.SetValue(_levelConfigProvider.LevelConfig.LeftItem);
            
            var boxRight = await _boxFactory.Create();
            boxRight.transform.position = rightBoxPoint.position;
            await boxRight.SetValue(_levelConfigProvider.LevelConfig.RightItem);

            boxRight.OnSelect
                .Subscribe(_ => _conveyorGameStateMachine.Enter<NextItemDeliveryState>().Forget())
                .AddTo(_disposable);
        }

        private async UniTask SetupStateMachine()
        {
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<BootstrapState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<NextItemDeliveryState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<SelectBoxState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<WinState>());
            _conveyorGameStateMachine.RegisterState(_stateFactory.Create<LoseState>());

            await _conveyorGameStateMachine.Enter<BootstrapState>();
        }
    }
}