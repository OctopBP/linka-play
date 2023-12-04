using LanguageExt;
using UnityEngine;

namespace Infrastructure.CursorService
{
    public class UICursorSelectable : MonoBehaviour, ICursorSelectable
    {
        [SerializeField] private RectTransform rectTransform;

        public Option<Bounds> MaybeBounds =>
            new Bounds(rectTransform.rect.center, rectTransform.rect.size);
        
        public void OnHover() { }
        public void OnSelect() { }
    }
}