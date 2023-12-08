using LanguageExt;
using UnityEngine;

namespace Infrastructure.CursorService
{
    public interface ICursorSelectable
    {
        Option<Bounds> MaybeBounds { get; }
        void OnHover();
        void OnSelect();
    }
}