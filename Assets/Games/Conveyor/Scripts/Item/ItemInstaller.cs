using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.Conveyor
{
	public class ItemInstaller : MonoInstaller
	{
		[SerializeField] private ItemView itemView;
		
		public override void InstallBindings()
		{
			Container.Bind<ItemView>().FromInstance(itemView).AsSingle();
			Container.Bind<ItemStore>().FromNew().AsSingle();
			
			Container.Bind<IStateFactory>().To<StateFactory>().FromNew().AsSingle();
			Container.Bind<ItemStateMachine>().To<ItemStateMachine>().FromNew().AsSingle();
		}
	}
}