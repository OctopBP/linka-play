using Cysharp.Threading.Tasks;
using Infrastructure;
using static LanguageExt.Prelude;

namespace Game.Conveyor
{
    public class DestroyState : IItemState
    {
        private readonly ItemView _itemView;
        private readonly ILog _log;
        private readonly IItemFactory<ItemView> _itemFactory;
        private readonly ItemStore _itemStore;

        public DestroyState(
            ItemView itemView, ConveyorPath conveyorPath, ILog log, IItemFactory<ItemView> itemFactory,
            ItemStore itemStore
        )
        {
            _itemView = itemView;
            _log = log;
            _itemFactory = itemFactory;
            _itemStore = itemStore;
        }

        public async UniTask Enter()
        {
            _log.Log("DestroyState Enter()", LogTag.Item);

            _itemStore.MaybeTargetBoxPlace = None;
            _itemFactory.Release(_itemView);
        }

        public UniTask Exit() => default;
    }
}