using System;
using System.Collections.Generic;

namespace CodingHelmet.Optional
{
    public static class Option
    {
        private class OptionImpl<T>: IOption<T>
        {
            private T[] Data { get; }

            public OptionImpl(T value)
            {
                this.Data = new[] {value};
            }

            public OptionImpl()
            {
                this.Data = new T[0];
            }

            public void Do(Action<T> callback) => Array.ForEach(this.Data, callback);

            public IEnumerable<T> AsEnumerable() => this.Data;
        }

        public static IOption<T> Some<T>(T obj)
        {
            return new OptionImpl<T>(obj);
        }

        public static IOption<T> None<T>()
        {
            return new OptionImpl<T>();
        }
    }
}
