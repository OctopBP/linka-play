using LanguageExt;
using UnityEngine;
using static LanguageExt.Prelude;

namespace Extensions
{
    public static class ComponentsExt
    {
        public static Option<T> MaybeComponent<T>(this Component self) =>
            self.gameObject.TryGetComponent<T>(out var component) ? component : None;
        
        public static Option<T> MaybeComponent<T>(this GameObject self) =>
            self.TryGetComponent<T>(out var component) ? component : None;
    }
}