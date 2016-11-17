using System;
using System.Linq;
using CodingHelmet.Optional;
using Xunit;

namespace OptionTests
{
    public abstract class OptionInterfaceTests<T>
    {
        [Fact]
        public void Do_SomeReceivesAction_ActionIsInvoked()
        {
            bool callbackInvoked = false;
            Action<T> callback = obj => callbackInvoked = true;

            IOption<T> option = this.CreateSome(this.SampleValue);
            option.Do(callback);

            Assert.True(callbackInvoked);
        }

        [Fact]
        public void Do_SomeContainingObjectReceivesAction_ActionReceivesThatContainedObject()
        {
            T expectedArgument = this.SampleValue;
            T actualArgument = default(T);
            Action<T> callback = obj => actualArgument = obj;

            IOption<T> option = this.CreateSome(expectedArgument);
            option.Do(callback);

            Assert.True(this.AreSame(expectedArgument, actualArgument));
        }

        [Fact]
        public void Do_NoneReceivesAction_ActionNotInvoked()
        {
            bool callbackInvoked = false;
            Action<object> callback = obj => callbackInvoked = true;

            IOption<object> option = Option.None<object>();
            option.Do(callback);

            Assert.False(callbackInvoked);
        }

        [Fact]
        public void AsEnumerable_Some_ReturnsNonNull()
        {
            Assert.NotNull(this.CreateSome(this.SampleValue).AsEnumerable());
        }

        [Fact]
        public void AsEnumerable_None_ReturnsNonNull()
        {
            Assert.NotNull(this.CreateNone().AsEnumerable());
        }

        [Fact]
        public void AsEnumerable_Some_ReturnsOneElement()
        {
            Assert.Equal(1, this.CreateSome(this.SampleValue).AsEnumerable().Count());
        }

        [Fact]
        public void AsEnumerable_None_ReturnsZeroElements()
        {
            Assert.Equal(0, this.CreateNone().AsEnumerable().Count());
        }

        [Fact]
        public void AsEnumerable_SomeContainingObject_ResultContainsThatObject()
        {
            T value = this.SampleValue;
            Assert.True(this.CreateSome(value).AsEnumerable().Any(x => this.AreSame(value, x)));
        }

        protected abstract T SampleValue { get; }

        protected abstract IOption<T> CreateSome(T obj);
        protected abstract IOption<T> CreateNone();

        protected abstract bool AreSame(T a, T b);

    }
}
