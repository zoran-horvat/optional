using System;
using System.Collections;
using System.Collections.Generic;

namespace CodingHelmet.SampleApp.Common
{

    interface IOption<out T> : IEnumerable<T>
    {
        void Do(Action<T> action);
    }

    static class Option
    {
        private class OptionImpl<T> : IOption<T>
        {

            private T[] Data { get; }

            public OptionImpl(T[] data)
            {
                this.Data = data;
            }

            public IEnumerator<T> GetEnumerator() =>
                ((IEnumerable<T>)this.Data).GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() =>
                this.GetEnumerator();

            public void Do(Action<T> action) =>
                Array.ForEach(this.Data, action);

        }

        public static IOption<T> None<T>() =>
            new OptionImpl<T>(new T[0]);

        public static IOption<T> Some<T>(T value) =>
            new OptionImpl<T>(new[] { value });

    }
}
