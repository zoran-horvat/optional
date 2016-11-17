using System.Collections.Generic;
using CodingHelmet.Optional;

namespace CodingHelmet.SampleApp.Common
{
    static class DictionaryExtensions
    {
        public static IOption<TValue> TryGetValue<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary, TKey key)
        {
            TValue value;
            if (!dictionary.TryGetValue(key, out value))
                return Option.None<TValue>();
            return Option.Some(value);
        }
    }
}
