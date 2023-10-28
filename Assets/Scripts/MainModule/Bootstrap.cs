using Infrastructure;
using UnityEngine;
using Zenject;

namespace MainModule
{
    /// <summary>
    /// Main entry point.
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        ISceneLoader sceneLoader;
		
        [Inject]
        void Construct(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }
    }
}