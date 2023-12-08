using Zenject;

namespace Game.Conveyor
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.Bind<IGameBootstrap>().To<ConveyorBootstrap>().FromNew().AsSingle();
		}
	}

	public class ConveyorBootstrap : IGameBootstrap
	{
		[Inject]
		private void Construct()
		{
			
		}
		
		public void Init()
		{
			
		}
	}

	public interface IGameBootstrap
	{
		void Init();
	}
}