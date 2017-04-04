using System;
using System.Linq;
using CodingHelmet.Optional;
using Xunit;

namespace OptionTests
{
    public abstract class OptionInterfaceTests<T, TMap>
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

        [Fact]
        public void Map_SomeReceivesMappingToObject_ReturnsSomeOfThatObject()
        {
            TMap mappedValue = this.SampleMapToValue;

            IOption<T> option = this.CreateSome(this.SampleValue);
            IOption<TMap> mapped = option.Map(_ => mappedValue);

            Assert.Same(mappedValue, mapped.AsEnumerable().ElementAt(0));
        }

        [Fact]
        public void Map_NoneReceivesMappingToObject_ReturnsNone()
        {
            IOption<T> option = this.CreateNone();
            IOption<TMap> mapped = option.Map(_ => this.SampleMapToValue);

            Assert.Equal(0, mapped.AsEnumerable().Count());
        }

        [Fact]
        public void Map_SomeReceivesMappingFunction_PassesContainedValueToMappingFunction()
        {
            T expectedValue = this.SampleValue;
            T actualValue = default(T);

            IOption<T> option = this.CreateSome(expectedValue);
            IOption<T> mapped = option.Map(x => actualValue = x);

            Assert.True(this.AreSame(expectedValue, actualValue));
        }

        [Fact]
        public void Map_NoneReceivesMappingFunction_MappingFunctionNotCalled()
        {
            bool mappingInvoked = false;

            IOption<T> option = this.CreateNone();
            IOption<TMap> mapped = option.Map(_ =>
            {
                mappingInvoked = true;
                return default(TMap);
            });

            Assert.False(mappingInvoked);
        }

        [Fact]
        public void Collapse_SomeContainingValue_ReturnsContainedValue()
        {
            T expectedValue = this.SampleValue;
            IOption<T> option = this.CreateSome(expectedValue);
            T actualValue = option.Fold(() => this.AlternateSampleValue);

            Assert.True(this.AreSame(expectedValue, actualValue));
        }

        [Fact]
        public void Collapse_NoneReceivesMethodWhichReturnsAlternateValue_ReturnsThatValue()
        {
            T expectedValue = this.AlternateSampleValue;
            IOption<T> option = this.CreateNone();
            T actualValue = option.Fold(() => expectedValue);

            Assert.True(this.AreSame(expectedValue, actualValue));
        }

        protected abstract T SampleValue { get; }
        protected abstract T AlternateSampleValue { get; }
        protected abstract TMap SampleMapToValue { get; }

        protected abstract IOption<T> CreateSome(T obj);
        protected abstract IOption<T> CreateNone();

        protected abstract bool AreSame(T a, T b);

    }
}
