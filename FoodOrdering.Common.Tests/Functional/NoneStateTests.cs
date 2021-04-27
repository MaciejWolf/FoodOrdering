using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodOrdering.Common.Functional;
using Moq;
using Xunit;

namespace FoodOrdering.Common.Tests.Functional
{
    public interface IFoo
    {
        bool None();
        bool Some();
    }

    public class NoneStateTests
	{
        [Fact]
        public void Equals_ReturnsTrue_IfReferenceSameObject()
        {
            var ref1 = Option<string>.None();
            var ref2 = ref1;

            var result = ref1.Equals(ref2);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_IfNullOfContainedType()
        {
            var opt1 = Option<string>.None();
            string nullString = null;

            var result = opt1.Equals(nullString);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsTrue_IfNoneOfTheSameType()
        {
            var opt1 = Option<string>.None();
            var opt2 = Option<string>.None();

            var result = opt1.Equals(opt2);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_IfSomeOfTheSameType()
        {
            var opt1 = Option<string>.None();
            var opt2 = "str".AsOption();

            var result = opt1.Equals(opt2);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_IfNoneOfDifferentType()
        {
            var opt1 = Option<string>.None();
            var opt2 = Option<int>.None();

            var result = opt1.Equals(opt2);

            Assert.False(result);
        }

        [Fact]
        public void Match_NoneFunctionIsCalled()
        {
            var foo = new Mock<IFoo>();

            Option<string>.None().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            foo.Verify(x => x.None(), Times.Once);
        }

        [Fact]
        public void Match_SomeFunctionIsNotCalled()
        {
            var foo = new Mock<IFoo>();

            Option<string>.None().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Never);
        }

        [Fact]
        public void Match_Returns_NoneFunctionResult()
        {
            var expected = true;

            var foo = new Mock<IFoo>();
            foo.Setup(x => x.None()).Returns(expected);
            foo.Setup(x => x.Some()).Returns(!expected);

            var result = Option<string>.None().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenSome_ProvidedFunctionIsNotCalled()
        {
            var foo = new Mock<IFoo>();

            Option<string>.None().WhenSome(_ => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Never);
        }

        [Fact]
        public void WhenSome_ReturnsCallersInstance()
        {
            var caller = Option<string>.None();

            var result = caller.WhenSome(_ => { });

            Assert.Same(caller, result);
        }

        [Fact]
        public void WhenNone_ProvidedFunctionIsCalled()
        {
            var foo = new Mock<IFoo>();

            Option<string>.None().WhenNone(() => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Once);
        }

        [Fact]
        public void WhenNone_ReturnsCallersInstance()
        {
            var caller = Option<string>.None().AsOption();

            var result = caller.WhenNone(() => { });

            Assert.Same(caller, result);
        }

        [Fact]
        public void Select_ReturnsNoneOfResultType()
        {
            var result = Option<string>.None().Select(x => x.ToUpper());

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void SelectMany_ReturnsNoneOfResult()
        {
            var result = Option<Option<string>>.None().SelectMany(x => x.Select(x1 => x1));

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void Where_ReturnsSameInstance()
        {
            var caller = Option<int>.None();

            var result = caller.Where(x => x > 10);

            Assert.Same(caller, result);
        }

        [Fact]
        public void ValueOr_ReturnsAlternativeValue()
        {
            var expected = 123;

            var result = Option<int>.None().ValueOr(expected);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValueOr_ReturnsAlternativeValue_FromProvider()
        {
            var expected = 123;

            var result = Option<int>.None().ValueOr(() => expected);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValueOrNull_ReturnsNull()
        {
            string expected = null;

            var result = expected.AsOption().ValueOrNull();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToNullable_ReturnsNull()
        {
            int? nullable = null;

            var result = nullable.NullableToOption().ToNullable();

            Assert.True(!result.HasValue);
        }
    }
}
