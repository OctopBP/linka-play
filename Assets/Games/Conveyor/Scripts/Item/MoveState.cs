using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Game.Conveyor
{
    public class MoveState : IItemState
    {
        private readonly ItemStateMachine _stateMachine;
        private readonly Vector3 _to;
        private readonly Transform _item;
        
        public async UniTask Enter()
        {
            await _item.DOMove(_to, 1);
            _stateMachine.Enter<OnPlaceState>().Forget();
        }

        public UniTask Exit() => default;
    }
}