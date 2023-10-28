using Infrastructure;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] GameInfoSO[] gameInfos;
        
        [SerializeField] RectTransform buttonsParent;
        [SerializeField] ButtonWithText gameButtonsPrefab;

        SceneLoader sceneLoader;
        
        [Inject]
        void Construct(SceneLoader sceneLoader)
        {
            this.sceneLoader = sceneLoader;
        }
        
        void Start()
        {
            BuildMenu();
        }

        void BuildMenu()
        {
            foreach (var gameInfo in gameInfos)
            {
                var button = Instantiate(gameButtonsPrefab, parent: buttonsParent);
                button._buttonText.SetText(gameInfo._displayName);
                button._button.OnClickAsObservable().Subscribe(_ => sceneLoader.LoadScene(gameInfo._scene));
            }
        }
    }
}
