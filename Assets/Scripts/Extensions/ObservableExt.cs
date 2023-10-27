using System;
using LanguageExt;
using UniRx;

namespace Extensions
{
    public static class ObservableExt
    {
        public static IObservable<bool> WhereTrue(this IObservable<bool> source) => source.Where(value => value);
        public static IObservable<bool> WhereFalse(this IObservable<bool> source) => source.Where(value => !value);
        public static IDisposable Collect<T>(this IObservable<Option<T>> source, Action<T> onNext) =>
            source.Subscribe(maybeValue => maybeValue.IfSome(onNext));
    }
}