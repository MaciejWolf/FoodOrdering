using System;
using FoodOrdering.Common.Functional;
using Shouldly;
using Xunit;

namespace FoodOrdering.Common.Functional.Tests
{
	public class OptionTests
	{
        [Fact]
        public void AsOptional_FromValue_CreateSome()
        {
            var result = "Hello".AsOption();

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe("Hello");
        }

        [Fact]
        public void AsOptional_FromNull_CreateNone()
        {
            string nullString = null;

            var result = nullString.AsOption();

            result.HasValue.ShouldBeFalse();
        }

        [Fact]
        public void NullableToOption_FromNullableType_WithValue_CreateSomeOfValue()
        {
            int? value = 5;

            var result = value.NullableToOption();

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(5);
        }

        [Fact]
        public void NullableToOption_FromNullableType_WithNullValue_CreateNoneOfValue()
        {
            int? value = null;

            var result = value.NullableToOption();

            result.HasValue.ShouldBeFalse();
        }
    }
}
