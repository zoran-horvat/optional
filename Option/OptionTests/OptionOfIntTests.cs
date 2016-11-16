using CodingHelmet.Option;

namespace OptionTests
{
    public class OptionOfIntTests: OptionInterfaceTests<int>
    {
        protected override int SampleValue => 5;

        protected override IOption<int> CreateSome(int obj) => Option.Some(obj);

        protected override IOption<int> CreateNone() => Option.None<int>();

        protected override bool AreSame(int a, int b) => a == b;
    }
}
