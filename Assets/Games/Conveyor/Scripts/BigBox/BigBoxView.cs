using TMPro;
using UniRx;
using UnityEngine;

namespace Game.Conveyor
{
    public class BigBoxView : MonoBehaviour
    {
        [SerializeField] private TMP_Text text;

        public readonly Subject<Unit> OnSelect = new();

        public void Select() => OnSelect.OnNext(Unit.Default);
        
        public void SetText(ItemValue itemValue) => text.SetText(itemValue.ToString());
    }
}
