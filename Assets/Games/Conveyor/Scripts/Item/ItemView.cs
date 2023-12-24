using Cysharp.Threading.Tasks;
using Infrastructure;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using static LanguageExt.Prelude;

namespace Game.Conveyor
{
    public class ItemView : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        private IStateFactory _stateFactory;
        private ItemStateMachine _itemStateMachine;
        private IAssetProvider _assetProvider;
        private ItemStore _itemStore;

        [Inject]
        private void Construct(
            ItemStateMachine itemStateMachine, IStateFactory stateFactory, IAssetProvider assetProvider,
            ItemStore itemStore
        )
        {
            _itemStateMachine = itemStateMachine;
            _stateFactory = stateFactory;
            _assetProvider = assetProvider;
            _itemStore = itemStore;
        }

        private void Start()
        {
            SetupStateMachine();
        }

        private void SetupStateMachine()
        {
            _itemStateMachine.RegisterState(_stateFactory.Create<InitState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<OnPlaceState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<MoveToStopPointState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<MoveToBoxState>());
            _itemStateMachine.RegisterState(_stateFactory.Create<DestroyState>());
        }

        public UniTask Init()
        {
            return _itemStateMachine.Enter<InitState>();
        }

        public void MoveToBox(BoxPlace boxPlace)
        {
            _itemStore.MaybeTargetBoxPlace = boxPlace;
            _itemStateMachine.Enter<MoveToBoxState>().Forget();
        }
        
        public async UniTask SetValue(ItemValue itemValue)
        {
            image.sprite = await _assetProvider.Load<Sprite>(itemValue.SpriteName);
        }
    }
}