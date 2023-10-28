using Infrastructure;
using UnityEngine;
using Zenject;

namespace Core
{
    /// <summary>
    /// Main entry point.
    /// </summary>
    public class Bootstrap : MonoBehaviour
    {
        SceneLoader sceneLoader;
		
        [Inject]
        void Construct(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }
    }
}