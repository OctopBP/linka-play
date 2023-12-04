using Zenject;

namespace Infrastructure.States
{
	public class StateFactory : IStateFactory
	{
		private readonly DiContainer _diContainer;
		
		public StateFactory(DiContainer diContainer)
		{
			_diContainer = diContainer;
		}

		public TState Create<TState>() where TState : class, IExitableState =>
			_diContainer.Instantiate<TState>();
	}
}

