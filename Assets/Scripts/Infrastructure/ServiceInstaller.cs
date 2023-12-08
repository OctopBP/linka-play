using Infrastructure.Input;
using Zenject;

namespace Infrastructure
{
    public class ServiceInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IRnd>().FromInstance(RandomAdapter.a(seed: 12345)).AsSingle();
            Container.Bind<SceneLoader>().FromNew().AsSingle();
            Container.Bind<IRaycastService>().To<RaycastService>().AsSingle();
            Container.Bind<ICameraService>().To<CameraService>().AsSingle();
            Container.Bind<IInput>().To<MouseInput>().AsSingle();
        }
    }
}
