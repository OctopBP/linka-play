using Infrastructure.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace Infrastructure
{
    public class InputInstaller : MonoInstaller
    {
        [SerializeField] EventSystem eventSystem;
        
        public override void InstallBindings()
        {
            Container.Bind<IInput>().FromInstance(new MouseInput(eventSystem: eventSystem)).AsSingle();
        }
    }
}