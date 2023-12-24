using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure;

namespace Game.Conveyor
{
    public class MoveToBoxState : IItemState
    {
        private readonly ItemView _itemView;
        private readonly ConveyorPath _conveyorPath;
        private readonly ILog _log;
        private readonly ItemStateMachine _itemStateMachine;
        private readonly ItemStore _itemStore;

        public MoveToBoxState(
            ItemView itemView, ConveyorPath conveyorPath, ILog log, ItemStore itemStore,
            ItemStateMachine itemStateMachine
        )
        {
            _itemView = itemView;
            _conveyorPath = conveyorPath;
            _log = log;
            _itemStore = itemStore;
            _itemStateMachine = itemStateMachine;
        }

        public async UniTask Enter()
        {
            _log.Log("MoveToBoxState Enter()", LogTag.Item);

            var maybePoints = _itemStore.MaybeTargetBoxPlace.
                Map(targetBoxPlace => _conveyorPath.GetSidePoints(targetBoxPlace));

            await maybePoints.IfSomeAsync(async points =>
            {
                foreach (var point in points)
                {
                    await _itemView.transform.DOMove(point, 1f);
                }
            });

            await _itemStateMachine.Enter<DestroyState>();
        }

        public UniTask Exit() => default;
    }
}