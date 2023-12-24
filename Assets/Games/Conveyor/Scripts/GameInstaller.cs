using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
	public class GameInstaller : MonoInstaller
	{
		[SerializeField] private LevelConfigProvider levelConfigProvider;
		[SerializeField] private ConveyorBootstrap conveyorBootstrap;
		[SerializeField] private ConveyorPath conveyorPath;
		[SerializeField] private GameAssets gameAssets;
		
		public override void InstallBindings()
		{
			Container.Bind<IGameBootstrap>().FromInstance(conveyorBootstrap).AsSingle();
			Container.Bind<ConveyorPath>().FromInstance(conveyorPath).AsSingle();
			Container.Bind<LevelConfigProvider>().FromInstance(levelConfigProvider).AsSingle();
			Container.Bind<GameAssets>().FromInstance(gameAssets).AsSingle();
			
			Container.Bind<IStateFactory>().To<StateFactory>().FromNew().AsSingle();
			Container.Bind<ConveyorGameStateMachine>().To<ConveyorGameStateMachine>().FromNew().AsSingle();
			
			Container.Bind<BoxesStore>().To<BoxesStore>().FromNew().AsSingle();
			
			Container.Bind<IItemFactory<ItemView>>().To<ItemsFactoryWithPool<ItemView>>().FromNew().AsSingle();
			Container.Bind<IItemFactory<BigBoxView>>().To<ItemsFactoryWithPool<BigBoxView>>().FromNew().AsSingle();
		}
	}
}