using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private ConveyorBootstrap conveyorBootstrap;
		[SerializeField] private ConveyorPath conveyorPath;
		
		public override void InstallBindings()
		{
			Container.Bind<IGameBootstrap>().FromInstance(conveyorBootstrap).AsSingle();
			Container.Bind<ConveyorPath>().FromInstance(conveyorPath).AsSingle();
			
			Container.Bind<IStateFactory>().To<StateFactory>().FromNew().AsSingle();
			Container.Bind<ConveyorGameStateMachine>().To<ConveyorGameStateMachine>().FromNew().AsSingle();
			Container.Bind<IItemFactory<ItemOnConveyor>>().To<ItemsFactory>().FromNew().AsSingle();
		}
	}
}