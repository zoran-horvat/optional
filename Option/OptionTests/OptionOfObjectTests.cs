using CodingHelmet.Option;

namespace OptionTests
{
    public class OptionOfObjectTests: OptionInterfaceTests<object>
    {
        protected override object SampleValue => new object();

        protected override IOption<object> CreateSome(object obj) => Option.Some(obj);

        protected override IOption<object> CreateNone() => Option.None<object>();

        protected override bool AreSame(object a, object b) => object.ReferenceEquals(a, b);
    }
}
