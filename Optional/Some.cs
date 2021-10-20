using System;
using System.Collections.Generic;

namespace CodingHelmet.Optional
{
    public sealed class Some<T> : Option<T>, IEquatable<Some<T>>
    {
        public T Content { get; }

        public Some(T value)
        {
            this.Content = value;
        }

        public static implicit operator T(Some<T> some) =>
            some.Content;

        public static implicit operator Some<T>(T value) =>
            new Some<T>(value);

        public override Option<TResult> Map<TResult>(Func<T, TResult> map) =>
            map(this.Content);

        public override Option<TResult> MapOptional<TResult>(Func<T, Option<TResult>> map) =>
            map(this.Content);

        public override T Reduce(T whenNone) =>
            this.Content;

        public override T Reduce(Func<T> whenNone) =>
            this.Content;

        public override Option<TNew> OfType<TNew>() => 
            typeof(T).IsAssignableFrom(typeof(TNew)) ? new Some<TNew>(this.Content as TNew)
            : (Option<TNew>)new None<TNew>();

        public override string ToString() =>
            $"Some({this.ContentToString})";

        private string ContentToString =>
            this.Content?.ToString() ?? "<null>";

        public bool Equals(Some<T> other) =>
            other?.GetType() == typeof(Some<T>) && 
            EqualityComparer<T>.Default.Equals(Content, other.Content);

        public override bool Equals(object obj) =>
            this.Equals(obj as Some<T>);

        public override int GetHashCode() =>
            this.Content?.GetHashCode() ?? 0;

        public static bool operator ==(Some<T> a, Some<T> b) =>
            a?.Equals(b) ?? b is null;

        public static bool operator !=(Some<T> a, Some<T> b) => !(a == b);
    }
}