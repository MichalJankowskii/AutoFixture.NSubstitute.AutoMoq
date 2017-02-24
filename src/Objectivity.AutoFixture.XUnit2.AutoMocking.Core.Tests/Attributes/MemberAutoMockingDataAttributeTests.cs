namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests.Attributes
{
    using System;
    using System.Collections.Generic;
    using Core.Customizations;
    using FluentAssertions;
    using Helpers;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    [Collection("MemberAutoMockingDataAttribute")]
    [Trait("Category", "Attributes")]
    public class MemberAutoMockingDataAttributeTests
    {
        public static IEnumerable<object[]> TestData { get; } = new[]
        {
            new object[] {1, 2, 3},
            new object[] {4, 5, 6},
            new object[] {7, 8, 9}
        };

        public int TestMethod(int first, int second, int third, int fourth, IDisposable disposable)
        {
            if (disposable == null)
            {
                throw new ArgumentNullException(nameof(disposable));
            }

            disposable.Dispose();
            return first + second + third + fourth;
        }

        [Theory(DisplayName = "WHEN constructor is invoked THEN has shared fixture")]
        [AutoData]
        public void WhenConstructorIsInvoked_ThenHasSharedFixture(Fixture fixture)
        {
            // Arrange
            // Act
            var attribute = new MemberAutoMockingTestHelperDataAttribute(fixture.Create<string>());

            // Assert
            attribute.Fixture.Should().NotBeNull();
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.ShareFixture.Should().BeTrue();
        }

        [Theory(DisplayName = "GIVEN IgnoreVirtualMembers WHEN GetData is invoked THEN fixture is customized correctly")]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void GivenIgnoreVirtualMembers_WhenGetDataIsInvoked_ThenFixtureIsCustomizedCorrectly(bool ignoreVirtualMembers)
        {
            // Arrange
            var fixture = new Mock<IFixture>();
            var customizations = new List<ICustomization>();
            fixture.Setup(x => x.Customize(It.IsAny<ICustomization>())).Callback<ICustomization>(customization => customizations.Add(customization));

            var attribute = new MemberAutoMockingTestHelperDataAttribute(fixture.Object, "TestData")
            {
                IgnoreVirtualMembers = ignoreVirtualMembers
            };
            var methodInfo = typeof(MemberAutoMockingDataAttributeTests).GetMethod("TestMethod");

            // Act
            attribute.GetData(methodInfo);

            // Assert
            customizations[0].Should().BeOfType<AutoMockingTestHelperDataCustomization>();
            customizations[1].Should().BeOfType<IgnoreVirtualMembersCustomization>();
            ((IgnoreVirtualMembersCustomization)customizations[1]).IgnoreVirtualMembers.Should().Be(ignoreVirtualMembers);
        }

        [Fact(DisplayName = "GIVEN uninitialized fixture WHEN constructor is invoked THEN exception is thrown")]
        public void GivenUninitializedFixture_WhenConstructorIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            var fixture = new Fixture();
            const Fixture uninitializedFixture = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new MemberAutoMockingTestHelperDataAttribute(uninitializedFixture, fixture.Create<string>()));
        }

        [Fact(DisplayName = "GIVEN uninitialized member name WHEN constructor is invoked THEN exception is thrown")]
        public void GivenUninitializedMemberName_WhenConstructorIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            const string memberName = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new MemberAutoMockingTestHelperDataAttribute(memberName));
        }
    }
}