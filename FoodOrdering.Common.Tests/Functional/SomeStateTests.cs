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
	public class SomeStateTests
	{
        [Fact]
        public void Equals_ReturnsTrue_IfReferenceSameObject()
        {
            var ref1 = 1.AsOption();
            var ref2 = ref1;

            var result = ref1.Equals(ref2);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnsTrue_IfContainedValuesAreEqual()
        {
            var opt1 = 1.AsOption();
            var opt2 = 1.AsOption();

            var result = opt1.Equals(opt2);

            Assert.True(result);
        }

        [Fact]
        public void Equals_ReturnFalse_IfContainedValuesAreNotEqual()
        {
            var opt1 = 1.AsOption();
            var opt2 = 2.AsOption();

            var result = opt1.Equals(opt2);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedWithContainedType()
        {
            var opt1 = 1.AsOption();
            var i = 2;

            var result = opt1.Equals(i);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedWithSomeContainingDifferentType()
        {
            var opt1 = 1.AsOption();
            var opt2 = "2".AsOption();

            var result = opt1.Equals(opt2);

            Assert.False(result);
        }

        [Fact]
        public void Equals_ReturnsFalse_WhenComparedWithNoneOfTheContainingType()
        {
            var opt1 = 1.AsOption();
            var opt2 = Option<int>.None();

            var result = opt1.Equals(opt2);

            Assert.False(result);
        }

        [Fact]
        public void Match_SomeFunctionIsCalled()
        {
            var foo = new Mock<IFoo>();

            "str".AsOption().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Once);
        }

        [Fact]
        public void Match_NoneFunctionIsNotCalled()
        {
            var foo = new Mock<IFoo>();

            "str".AsOption().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            foo.Verify(x => x.None(), Times.Never);
        }

        [Fact]
        public void Match_Returns_SomeFunctionResult()
        {
            var expected = true;

            var foo = new Mock<IFoo>();
            foo.Setup(x => x.Some()).Returns(expected);
            foo.Setup(x => x.None()).Returns(!expected);

            var result = "str".AsOption().Match(
                none: () => foo.Object.None(),
                some: _ => foo.Object.Some());

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WhenSome_ProvidedFunctionIsCalled()
        {
            var foo = new Mock<IFoo>();

            "str".AsOption().WhenSome(_ => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Once);
        }

        [Fact]
        public void WhenSome_ReturnsCallersInstance()
        {
            var caller = "str".AsOption();

            var result = caller.WhenSome(_ => { });

            Assert.Same(caller, result);
        }

        [Fact]
        public void WhenNone_ProvidedFunctionIsNotCalled()
        {
            var foo = new Mock<IFoo>();

            "str".AsOption().WhenNone(() => foo.Object.Some());

            foo.Verify(x => x.Some(), Times.Never);
        }

        [Fact]
        public void WhenNone_ReturnsCallersInstance()
        {
            var caller = "str".AsOption();

            var result = caller.WhenNone(() => { });

            Assert.Same(caller, result);
        }

        [Fact]
        public void Select_IfSelectedResultIsNotNull_ReturnsSomeOfResult()
        {
            var result = "str".AsOption().Select(x => x);

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void Select_IfSelectedResultIsNull_ReturnsNoneOfResult()
        {
            var result = "str".AsOption().Select(x => (string)null);

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void Select_IfSelectedResultIsNotNull_ReturnsSomeOfSelectedValue()
        {
            var expected = "123";

            var result = 123.AsOption().Select(x => x.ToString()).ValueOrNull();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SelectMany_IfOuterOptionIsNone_ReturnsNoneOfResult()
        {
            var innerOption = "str".AsOption();

            var result = Option<int>.None().SelectMany(x => innerOption.Select(s => $"{x}{s}"));

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void SelectMant_IfInnerOptionIsNone_ReturnsNoneOfResult()
        {
            var innerOption = Option<string>.None();

            var result = 123.AsOption().SelectMany(x => innerOption.Select(s => $"{x}{s}"));

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void SelectMany_IfSelectedResultIsNotNull_ReturnsSomeOfResult()
        {
            var innerOption = "str".AsOption();

            var result = 123.AsOption().SelectMany(x => innerOption.Select(s => $"{x}{s}"));

            Assert.IsType<Option<string>>(result);
        }

        [Fact]
        public void SelectMany_IfSelectedResultIsNotNull_ReturnsOptionOfSelectedResult()
        {
            var expected = "123str";

            var innerOption = "str".AsOption();

            var result = 123.AsOption().SelectMany(x => innerOption.Select(s => $"{x}{s}")).ValueOrNull();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Where_IfPredicatePassed_ReturnsSameInstance()
        {
            var caller = 100.AsOption();

            var result = caller.Where(x => x > 10);

            Assert.Same(caller, result);
        }

        [Fact]
        public void Where_IfPredicateFailed_ReturnsNone()
        {
            var caller = 10.AsOption();

            var result = caller.Where(x => x > 100);

            Assert.IsType<Option<int>>(result);
        }

        [Fact]
        public void ValueOr_ReturnsValue()
        {
            var expected = 123;

            var result = expected.AsOption().ValueOr(999);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ValueOrNull_ReturnsValue()
        {
            var expected = "str";

            var result = expected.AsOption().ValueOrNull();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void ToNullable_ReturnsNullableWithValue()
        {
            var result = 123.AsOption().ToNullable();

            Assert.True(result.HasValue);
        }
    }
}
