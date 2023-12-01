using Cysharp.Threading.Tasks;

namespace Infrastructure.States
{
	interface IStateMachine
	{
		UniTask Enter<TState>() where TState : class, IState;
		UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPaylodedState<TPayload>;
		void RegisterState<TState>(TState state) where TState : class, IExitableState;
	}
}