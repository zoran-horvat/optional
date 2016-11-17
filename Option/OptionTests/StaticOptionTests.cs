using CodingHelmet.Optional;
using Xunit;

namespace OptionTests
{
    public class StaticOptionTests
    {
        [Fact]
        public void SomeOfObject_ReceivesNonNullObject_ReturnsNonNull()
        {
            IOption<object> option = Option.Some(new object());
            Assert.NotNull(option);
        }

        [Fact]
        public void NoneOfObject_ReturnsNonNull()
        {
            IOption<object> option = Option.None<object>();
            Assert.NotNull(option);
        }
    }
}
