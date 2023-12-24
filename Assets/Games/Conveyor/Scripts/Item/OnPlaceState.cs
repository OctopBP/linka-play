using Cysharp.Threading.Tasks;
using Infrastructure;
using static LanguageExt.Prelude;

namespace Game.Conveyor
{
    public class OnPlaceState : IItemState
    {
        private readonly ILog _log;
        private readonly BoxesStore _boxesStore;
        private readonly ItemView _itemView;

        public OnPlaceState(ILog log, BoxesStore boxesStore, ItemView itemView)
        {
            _log = log;
            _boxesStore = boxesStore;
            _itemView = itemView;
        }

        public UniTask Enter()
        {
            _log.Log("OnPlaceState Enter()", LogTag.Item);
            _boxesStore.ActiveBoxes = _itemView;
            
            return default;
        }

        public UniTask Exit()
        {
            _boxesStore.ActiveBoxes = None;

            return default;
        }
    }
}