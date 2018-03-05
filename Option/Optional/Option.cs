using System;

namespace CodingHelmet.Optional
{
    public abstract class Option<T>
    {
        public static implicit operator Option<T>(T value) =>
            new Some<T>(value);

        public static implicit operator Option<T>(None none) =>
            new None<T>();

        public abstract Option<TResult> Map<TResult>(Func<T, TResult> map);
        public abstract Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map);
        public abstract T Reduce(T whenNone);
        public abstract T Reduce(Func<T> whenNone);

        public Option<TNew> OfType<TNew>() where TNew : class =>
            this is Some<T> some && typeof(TNew).IsAssignableFrom(typeof(T))
                ? (Option<TNew>)new Some<TNew>(some.Content as TNew)
                : None.Value;
    }
}
