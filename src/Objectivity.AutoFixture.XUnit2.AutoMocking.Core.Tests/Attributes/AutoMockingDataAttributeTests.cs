namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Core.Attributes;
    using Core.Customizations;
    using Core.Providers;
    using FluentAssertions;
    using Helpers;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;
    using Xunit.Sdk;

    [Collection("AutoMockingDataAttribute")]
    [Trait("Category", "Attributes")]
    public class AutoMockingDataAttributeTests
    {
        public void TestMethod()
        {
        }

        [Fact(DisplayName = "WHEN parameterless constructor is invoked THEN fixture and attribute provider are created")]
        public void WhenParameterlessConstructorIsInvoked_ThenFixtureAndAttributeProviderAreCreated()
        {
            // Arrange
            // Act
            var attribute = new AutoMockingTestHelperDataAttribute();

            // Assert
            attribute.Fixture.Should().NotBeNull();
            attribute.Provider.Should().NotBeNull();
            attribute.IgnoreVirtualMembers.Should().BeFalse();
        }

        [Theory(DisplayName = "GIVEN existing fixture and attribute provider WHEN constructor is invoked THEN has specified fixture and attribute provider")]
        [AutoData]
        public void GivenExistingFixtureAndAttributeProvider_WhenConstructorIsInvoked_ThenHasSpecifiedFixtureAndAttributeProvider(Fixture fixture)
        {
            // Arrange
            var provider = new Mock<IAutoFixtureAttributeProvider>().Object;

            // Act
            var attribute = new AutoMockingTestHelperDataAttribute(fixture, provider);

            // Assert
            attribute.Fixture.Should().Be(fixture);
            attribute.Provider.Should().Be(provider);
            attribute.IgnoreVirtualMembers.Should().BeFalse();
        }

        [Fact(DisplayName = "GIVEN uninitialized fixture WHEN constructor is invoked THEN exception is thrown")]
        public void GivenUninitializedFixture_WhenConstructorIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            const Fixture fixture = null;
            var provider = new Mock<IAutoFixtureAttributeProvider>().Object;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new AutoMockingTestHelperDataAttribute(fixture, provider));
        }

        [Theory(DisplayName = "GIVEN uninitialized attribute provider WHEN constructor is invoked THEN exception is thrown")]
        [AutoData]
        public void GivenUninitializedAttributeProvider_WhenConstructorIsInvoked_ThenExceptionIsThrown(Fixture fixture)
        {
            // Arrange
            const IAutoFixtureAttributeProvider provider = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new AutoMockingTestHelperDataAttribute(fixture, provider));
        }

        [Theory(DisplayName = "WHEN GetData is invoked THEN fixture is configured and data returned")]
        [InlineAutoData(true)]
        [InlineAutoData(false)]
        public void WhenGetDataIsInvoked_ThenFixtureIsConfiguredAndDataReturned(bool ignoreVirtualMembers)
        {
            // Arrange
            var data = new[]
            {
                new object[] {1, 2, 3},
                new object[] {4, 5, 6},
                new object[] {7, 8, 9}
            };
            var fixture = new Mock<IFixture>();
            var customizations = new List<ICustomization>();
            fixture.Setup(x => x.Customize(It.IsAny<ICustomization>())).Callback<ICustomization>(customization => customizations.Add(customization));
            var dataAttribute = new Mock<DataAttribute>();
            dataAttribute.Setup(a => a.GetData(It.IsAny<MethodInfo>())).Returns(data);
            var provider = new Mock<IAutoFixtureAttributeProvider>();
            provider.Setup(p => p.GetAttribute(It.IsAny<IFixture>())).Returns(dataAttribute.Object);
            var attribute = new AutoMockingTestHelperDataAttribute(fixture.Object, provider.Object)
            {
                IgnoreVirtualMembers = ignoreVirtualMembers
            };
            var methodInfo = typeof(AutoMockingDataAttributeTests).GetMethod("TestMethod");

            // Act
            var result = attribute.GetData(methodInfo);

            // Assert
            result.Should().BeSameAs(data);
            provider.VerifyAll();
            dataAttribute.VerifyAll();

            customizations[0].Should().BeOfType<AutoMockingTestHelperDataCustomization> ();
            customizations[1].Should().BeOfType<IgnoreVirtualMembersCustomization>();
            ((IgnoreVirtualMembersCustomization)customizations[1]).IgnoreVirtualMembers.Should().Be(ignoreVirtualMembers);
        }
    }
}
