namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Tests.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using AutoMocking.Core.Customizations;
    using AutoMocking.Core.Providers;
    using AutoNSubstitute.Attributes;
    using AutoNSubstitute.Customizations;
    using FluentAssertions;
    using Moq;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;
    using Xunit.Sdk;

    [Collection("InlineAutoNSubstituteDataAttribute")]
    [Trait("Category", "NSubstitute")]
    public class InlineAutoNSubstituteDataAttributeTests
    {
        [Fact(DisplayName = "WHEN parameterless constructor is invoked THEN has no values but fixture and attribute provider are created")]
        public void WhenParameterlessConstructorIsInvoked_ThenHasNoValuesButFixtureAndAttributeProviderAreCreated()
        {
            // Arrange
            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute();

            // Assert
            attribute.Fixture.Should().NotBeNull();
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().NotBeNull();
            attribute.Values.Should().HaveCount(0);
        }

        [Fact(DisplayName = "GIVEN existing inline values WHEN constructor is invoked THEN has specified values and fixture and attribute provider are created")]
        public void GivenExistingInlineValues_WhenConstructorIsInvoked_ThenHasSpecifiedValuesAndFixtureAndAttributeProviderAreCreated()
        {
            // Arrange
            var initialValues = new[] {"test", 1, new object()};

            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute(initialValues[0], initialValues[1], initialValues[2]);

            // Assert
            attribute.Fixture.Should().NotBeNull();
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().NotBeNull();
            attribute.Values.Should().BeEquivalentTo(initialValues);
        }

        [Fact(DisplayName = "GIVEN uninitialized values WHEN constructor is invoked THEN has no values and fixture and attribute provider are created")]
        public void GivenUninitializedValues_WhenConstructorIsInvoked_ThenHasNoValuesAndFixtureAndAttributeProviderAreCreated()
        {
            // Arrange
            const object[] initialValues = null;

            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute(initialValues);

            // Assert
            attribute.Fixture.Should().NotBeNull();
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().NotBeNull();
            attribute.Values.Should().HaveCount(0);
        }

        [Theory(DisplayName = "GIVEN existing fixture and attribute provider WHEN constructor is invoked THEN has fixture attribute provider and no values")]
        [AutoData]
        public void GivenExistingFixtureAndAttributeProvider_WhenConstructorIsInvoked_ThenHasFixtureAttributeProviderAndNoValues(Fixture fixture)
        {
            // Arrange
            var provider = new Mock<IAutoFixtureInlineAttributeProvider>().Object;

            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute(fixture, provider);

            // Assert
            attribute.Fixture.Should().Be(fixture);
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().Be(provider);
            attribute.Values.Should().HaveCount(0);
        }

        [Theory(DisplayName = "GIVEN existing fixture, attribute provider and values WHEN constructor is invoked THEN has specified fixture, attribute provider and values")]
        [AutoData]
        public void GivenExistingFixtureAttributeProviderAndValues_WhenConstructorIsInvoked_ThenHasSpecifiedFixtureAttributeProviderAndValues(Fixture fixture)
        {
            // Arrange
            var provider = new Mock<IAutoFixtureInlineAttributeProvider>().Object;
            var initialValues = new[] { "test", 1, new object() };

            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute(fixture, provider, initialValues[0], initialValues[1], initialValues[2]);

            // Assert
            attribute.Fixture.Should().Be(fixture);
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().Be(provider);
            attribute.Values.Should().BeEquivalentTo(initialValues);
        }

        [Theory(DisplayName = "GIVEN existing fixture, attribute provider and uninitialized values WHEN constructor is invoked THEN has specified fixture, attribute provider and no values")]
        [AutoData]
        public void GivenExistingFixtureAttributeProviderAndUninitializedValues_WhenConstructorIsInvoked_ThenHasSpecifiedFixtureAttributeProviderAndNoValues(Fixture fixture)
        {
            // Arrange
            var provider = new Mock<IAutoFixtureInlineAttributeProvider>().Object;
            const object[] initialValues = null;

            // Act
            var attribute = new InlineAutoNSubstituteDataAttribute(fixture, provider, initialValues);

            // Assert
            attribute.Fixture.Should().Be(fixture);
            attribute.IgnoreVirtualMembers.Should().BeFalse();
            attribute.Provider.Should().Be(provider);
            attribute.Values.Should().HaveCount(0);
        }

        [Fact(DisplayName = "GIVEN uninitialized fixture WHEN constructor is invoked THEN exception is thrown")]
        [AutoData]
        public void GivenUninitializedFixture_WhenConstructorIsInvoked_ThenExceptionIsThrown()
        {
            // Arrange
            const Fixture fixture = null;
            var provider = new Mock<IAutoFixtureInlineAttributeProvider>().Object;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InlineAutoNSubstituteDataAttribute(fixture, provider));
        }

        [Theory(DisplayName = "GIVEN uninitialized attribute provider WHEN constructor is invoked THEN exception is thrown")]
        [AutoData]
        public void GivenUninitializedAttributeProvider_WhenConstructorIsInvoked_ThenExceptionIsThrown(Fixture fixture)
        {
            // Arrange
            const IAutoFixtureInlineAttributeProvider provider = null;

            // Act
            // Assert
            Assert.Throws<ArgumentNullException>(() => new InlineAutoNSubstituteDataAttribute(fixture, provider));
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
            var provider = new Mock<IAutoFixtureInlineAttributeProvider>();
            provider.Setup(p => p.GetAttribute(It.IsAny<IFixture>())).Returns(dataAttribute.Object);
            var attribute = new InlineAutoNSubstituteDataAttribute(fixture.Object, provider.Object)
            {
                IgnoreVirtualMembers = ignoreVirtualMembers
            };
            var methodInfo = typeof(AutoNSubstituteDataAttributeTests).GetMethod("TestMethod");

            // Act
            var result = attribute.GetData(methodInfo);

            // Assert
            result.Should().BeSameAs(data);
            provider.VerifyAll();
            dataAttribute.VerifyAll();

            customizations[0].Should().BeOfType<AutoNSubstituteDataCustomization>();
            customizations[1].Should().BeOfType<IgnoreVirtualMembersCustomization>();
            ((IgnoreVirtualMembersCustomization)customizations[1]).IgnoreVirtualMembers.Should().Be(ignoreVirtualMembers);
        }

        [InlineAutoNSubstituteData(100)]
        [Theory(DisplayName = "GIVEN test method has some inline parameters WHEN test run THEN parameters are generated")]
        public void GivenTestMethodHasSomeInlineParameters_WhenTestRun_ThenParametersAreGenerated(int value, IDisposable disposable)
        {
            // Arrange
            // Act
            // Assert
            value.Should().Be(100);

            disposable.Should().NotBeNull();
            disposable.GetType().Name.Should().StartWith("IDisposableProxy", "that way we know it was mocked with NSubstitute.");
        }
    }
}
