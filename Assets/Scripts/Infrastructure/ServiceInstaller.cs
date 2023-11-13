using Infrastructure.Input;
using Zenject;

namespace Infrastructure
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRnd>().FromInstance(RandomAdapter.a(seed: 12345)).AsSingle();
            Container.Bind<SceneLoader>().FromInstance(new()).AsSingle();
        }
    }
}
