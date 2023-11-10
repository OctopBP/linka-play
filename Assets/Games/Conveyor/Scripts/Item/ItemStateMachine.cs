using DG.Tweening;
using Game.Core;
using LanguageExt;
using UniRx;
using UnityEngine;

namespace Game.Conveyor
{
    public interface IItemState : IState
    {
        
    }
    
    public class ItemStateMachine : BaseStateMachine<IItemState>
    {
        public ItemStateMachine(ConveyorPath path, ItemOnConveyor item)
        {
            AddState(new MoveState(this, path._stopPoint, item.transform));
            AddState(new OnPlaceState());
        }
    }

    [GenConstructor]
    public partial class MoveState : IItemState
    {
        readonly ItemStateMachine stateMachine;
        readonly Vector3 to;
        readonly Transform item;
        
        public void OnEnter()
        {
            item.DOMove(to, 1)
                .OnComplete(() => stateMachine.EnterState<OnPlaceState>());
        }

        public void OnExit()
        {

        }
    }
    
    [GenConstructor]
    public partial class OnPlaceState : IItemState
    {
        public void OnEnter()
        {
        }

        public void OnExit()
        {
        }
    }
    
}

