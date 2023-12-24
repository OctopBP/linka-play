using Cysharp.Threading.Tasks;
using DG.Tweening;
using Infrastructure;

namespace Game.Conveyor
{
    public class MoveToStopPointState : IItemState
    {
        private readonly ItemStateMachine _stateMachine;
        private readonly ItemView _itemView;
        private readonly ConveyorPath _conveyorPath;
        private readonly ILog _log;

        public MoveToStopPointState(
            ItemStateMachine stateMachine, ItemView itemView, ConveyorPath conveyorPath, ILog log
        )
        {
            _stateMachine = stateMachine;
            _itemView = itemView;
            _conveyorPath = conveyorPath;
            _log = log;
        }

        public async UniTask Enter()
        {
            _log.Log("MoveToStopPointState Enter()", LogTag.Item);
            
            var point = _conveyorPath.StopPoint;
            await _itemView.transform.DOMove(point, 1);

            await _stateMachine.Enter<OnPlaceState>();
        }

        public UniTask Exit() => default;
    }
}