using System.Collections.Generic;
using System.Linq;
using CodingHelmet.Optional;

namespace CodingHelmet.SampleApp.Common
{
    static class EnumerableExtensions
    {
        public static IOption<T> AsOption<T>(this IEnumerable<T> sequence) =>
            sequence
                .Select(el => Option.Some(el))
                .DefaultIfEmpty(Option.None<T>())
                .Single();
    }
}
