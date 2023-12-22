using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Game.Conveyor
{
    public class MoveToBoxState : IItemState
    {
        private readonly ItemStateMachine _stateMachine;
        private readonly ItemView _itemView;
        private readonly ConveyorPath _conveyorPath;

        public MoveToBoxState(ItemStateMachine stateMachine, ItemView itemView, ConveyorPath conveyorPath)
        {
            _stateMachine = stateMachine;
            _itemView = itemView;
            _conveyorPath = conveyorPath;
        }

        public async UniTask Enter()
        {
            await _itemView.transform.DOMove(_conveyorPath.LeftPointUp, 1);
            await _itemView.transform.DOMove(_conveyorPath.LeftPointDown, 1);

            _stateMachine.Enter<OnPlaceState>().Forget();
        }

        public UniTask Exit() => default;
    }
}