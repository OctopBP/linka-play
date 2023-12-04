using Infrastructure.States;

namespace Core
{
	public interface IGameState : IState { }
	public class GameStateMachine : StateMachine<IGameState> { }
}