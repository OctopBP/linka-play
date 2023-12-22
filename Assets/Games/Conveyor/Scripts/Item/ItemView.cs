using Cysharp.Threading.Tasks;
using Infrastructure.States;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
    public class ItemView : MonoBehaviour, IItemView
    {
        [SerializeField] private TMP_Text text;
        
        private IStateFactory _stateFactory;
        private ItemStateMachine _itemStateMachine;

        [Inject]
        private void Construct(ItemStateMachine itemStateMachine, IStateFactory stateFactory)
        {
            _stateFactory = stateFactory;
            _itemStateMachine = itemStateMachine;
        }

        private async void Start()
        {
            await SetupStateMachine();
        }

        private async UniTask SetupStateMachine()
        {
            _itemStateMachine.RegisterState(_stateFactory.Create<InitState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<OnPlaceState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<MoveToStopPointState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<MoveToBoxState>());

            await _itemStateMachine.Enter<InitState>();
        }
        
        public void SetText(ItemValue itemValue) => text.SetText(itemValue.Value.ToString());
    }
}