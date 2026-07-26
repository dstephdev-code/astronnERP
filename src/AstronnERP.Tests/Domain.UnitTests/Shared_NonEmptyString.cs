using AstronnERP.Domain.SharedObjects.ValueObjects;

namespace AstronnERP.Domain.UnitTests
{
    public class Shared_NonEmptyString
    {
        [Fact]
        public void NonEmptyString_WhenEmpty_ShouldFail()
        {
            var result = NonEmptyString.Create("", "Result");

            Assert.IsNotType<NonEmptyString>(result);
            Assert.True(result.IsFailed);
        }
        [Fact]
        public void NonEmptyString_WhenWhitespaces_ShouldFail()
        {
            var result = NonEmptyString.Create("    ", "Result");

            Assert.IsNotType<NonEmptyString>(result);
            Assert.True(result.IsFailed);
        }
        [Fact]
        public void NonEmptyString_WhenValid_ShouldSuccess()
        {
            var result = NonEmptyString.Create("Valid String", "Result");

            Assert.IsType<NonEmptyString>(result.ValueOrDefault);
            Assert.True(result.IsSuccess);
            Assert.Equal("Valid String", result.Value.Value);
        }
    }
}
