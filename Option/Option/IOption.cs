using System;
using System.Collections.Generic;

namespace CodingHelmet.Optional
{
    public interface IOption<T>
    {
        void Do(Action<T> callback);
        IEnumerable<T> AsEnumerable();
    }
}
