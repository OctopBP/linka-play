using System;
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
        private BoxesStore _boxesStore;

        private readonly CompositeDisposable _disposable = new();
        
        [Inject]
        private void Construct(
            ConveyorGameStateMachine conveyorGameStateMachine, IStateFactory stateFactory,
            IItemFactory<ItemView> itemFactory, IItemFactory<BigBoxView> boxFactory,
            LevelConfigProvider levelConfigProvider, BoxesStore boxesStore
        )
        {
            _stateFactory = stateFactory;
            _conveyorGameStateMachine = conveyorGameStateMachine;
            _itemFactory = itemFactory;
            _boxFactory = boxFactory;
            _levelConfigProvider = levelConfigProvider;
            _boxesStore = boxesStore;
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
            await SetupBigBox(spawnPoint: rightBoxPoint, boxPlace: BoxPlace.Right, getValue: lc => lc.RightItem);
            await SetupBigBox(spawnPoint: leftBoxPoint, boxPlace: BoxPlace.Left, getValue: lc => lc.LeftItem);
        }

        private async UniTask SetupBigBox(Transform spawnPoint, BoxPlace boxPlace, Func<LevelConfig, ItemValue> getValue)
        {
            var nexBox = _boxFactory.Create();
            nexBox.transform.position = spawnPoint.position;
            nexBox.transform.rotation = spawnPoint.rotation;
            
            await nexBox.SetValue(getValue(_levelConfigProvider.LevelConfig));

            nexBox.OnSelect
                .Subscribe(_ =>
                {
                    _boxesStore.ActiveBoxes.IfSome(box => box.MoveToBox(boxPlace));
                    _conveyorGameStateMachine.Enter<NextItemDeliveryState>().Forget();
                })
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