using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;

namespace MainModule
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] Button[] gameButtons;

        ISceneLoader sceneLoader;
        
        [Inject]
        void Construct(ISceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }
        
        void Start()
        {
            for (var i = 0; i < gameButtons.Length; i++)
            {
                var gameButton = gameButtons[i];
                gameButton.OnClickAsObservable().Subscribe(_ =>
                {
                    // sceneLoader
                });
            }
        }
    }
}
