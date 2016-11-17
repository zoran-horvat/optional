using System;
using System.Collections.Generic;

namespace CodingHelmet.Optional
{
    public interface IOption<T>
    {
        void Do(Action<T> callback);
        IOption<TResult> Map<TResult>(Func<T, TResult> mapping);
        IEnumerable<T> AsEnumerable();
    }
}
