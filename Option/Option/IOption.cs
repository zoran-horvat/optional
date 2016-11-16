using System;
using System.Collections.Generic;

namespace CodingHelmet.Option
{
    public interface IOption<T>
    {
        void Do(Action<T> callback);
        IEnumerable<T> AsEnumerable();
    }
}
