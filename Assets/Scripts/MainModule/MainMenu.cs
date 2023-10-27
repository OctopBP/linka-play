using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Button[] gameButtons;
    
    void Start()
    {
        foreach (var gameButton in gameButtons)
        {
            gameButton.OnClickAsObservable().Subscribe(_ =>
            {
                // TODO: load game
            });
        }
    }
}
