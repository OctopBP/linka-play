using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Conveyor
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        private IStateFactory _stateFactory;
        private ItemStateMachine _itemStateMachine;
        private IAssetProvider _assetProvider;

        [Inject]
        private void Construct(
            ItemStateMachine itemStateMachine, IStateFactory stateFactory, IAssetProvider assetProvider
        )
        {
            _itemStateMachine = itemStateMachine;
            _stateFactory = stateFactory;
            _assetProvider = assetProvider;
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
        
        public async UniTask SetValue(ItemValue itemValue)
        {
            image.sprite = await _assetProvider.Load<Sprite>(itemValue.EmojiSprite);
        }
    }
}