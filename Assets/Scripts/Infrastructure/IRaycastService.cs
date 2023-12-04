using LanguageExt;
using UnityEngine;

namespace Infrastructure
{
    public interface IRaycastService
    {
        Option<TComponent> RaycastFromCameraToPosition<TComponent>(Camera camera, Vector2 screenPosition);
    }
}