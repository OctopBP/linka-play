namespace Infrastructure.States
{
	public class StateFactory : IStateFactory
	{
		public TState Create<TState>() where TState : class, IExitableState, new() => new();
	}
}

