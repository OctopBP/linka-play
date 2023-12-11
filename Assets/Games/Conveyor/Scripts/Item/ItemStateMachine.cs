using Infrastructure.States;

namespace Game.Conveyor
{
    public interface IItemState : IState { }
    
    public class ItemStateMachine : StateMachine<IItemState> { }
}

