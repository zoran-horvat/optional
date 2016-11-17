using CodingHelmet.Optional;

namespace OptionTests
{
    public class OptionOfObjectTests: OptionInterfaceTests<object, string>
    {
        private static object SampleObject = new object();
        private static object AlternateObject = new object();

        protected override object SampleValue => SampleObject;

        protected override object AlternateSampleValue => AlternateObject;

        protected override string SampleMapToValue => "something";

        protected override IOption<object> CreateSome(object obj) => Option.Some(obj);

        protected override IOption<object> CreateNone() => Option.None<object>();

        protected override bool AreSame(object a, object b) => object.ReferenceEquals(a, b);
    }
}
