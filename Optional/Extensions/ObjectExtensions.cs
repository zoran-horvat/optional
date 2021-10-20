using System;

namespace CodingHelmet.Optional.Extensions
{
    public static class ObjectExtensions
    {
        public static Option<T> When<T>(this T obj, bool condition) =>
            condition ? new Some<T>(obj)
            : (Option<T>)new None<T>();

        public static Option<T> When<T>(this T obj, Func<T, bool> predicate) =>
            obj.When(predicate(obj));

        public static Option<T> NoneIfNull<T>(this T obj) =>
            obj.When(!object.ReferenceEquals(obj, null));
    }
}
