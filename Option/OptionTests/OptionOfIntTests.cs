using System;
using CodingHelmet.Optional;

namespace OptionTests
{
    public class OptionOfIntTests: OptionInterfaceTests<int, string>
    {
        protected override int SampleValue => 5;

        protected override int AlternateSampleValue => 3;

        protected override string SampleMapToValue => "something";

        protected override IOption<int> CreateSome(int obj) => Option.Some(obj);

        protected override IOption<int> CreateNone() => Option.None<int>();

        protected override bool AreSame(int a, int b) => a == b;
    }
}
