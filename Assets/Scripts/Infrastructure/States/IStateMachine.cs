using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
	public interface IStateMachine<in TMachineState> where TMachineState : class, IState
	{
		UniTask Enter<TState>() where TState : class, TMachineState;
		void RegisterState<TState>(TState state) where TState : TMachineState;
	}
}