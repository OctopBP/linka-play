using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Conveyor
{
    public class MoveToStopPointState : IItemState
    {
        private readonly ItemStateMachine _stateMachine;
        private readonly ItemView _itemView;
        private readonly ConveyorPath _conveyorPath;

        public MoveToStopPointState(ItemStateMachine stateMachine, ItemView itemView, ConveyorPath conveyorPath)
        {
            _stateMachine = stateMachine;
            _itemView = itemView;
            _conveyorPath = conveyorPath;
        }

        public async UniTask Enter()
        {
            var point = _conveyorPath.StopPoint;
            await _itemView.transform.DOMove(point, 1);

            _stateMachine.Enter<OnPlaceState>().Forget();
        }

        public UniTask Exit() => default;
    }
}