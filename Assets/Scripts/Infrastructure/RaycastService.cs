using Extensions;
using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Infrastructure
{
    public class RaycastService : IRaycastService
    {
        public Option<TComponent> RaycastFromCameraToPosition<TComponent>(Camera camera, Vector2 screenPosition)
        {
            var ray = camera.ScreenPointToRay(screenPosition);
            Physics.Raycast(ray, out var hit, 10);
            return Optional(hit.transform).Map(t => t.MaybeComponent<TComponent>()).Flatten();
        }
    }
}