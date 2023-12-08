namespace Infrastructure.States
{
	public interface IStateFactory
	{
		public TState Create<TState>() where TState : class, IExitableState;
	}
}