namespace Objectivity.AutoFixture.XUnit2.AutoMoq.Tests.Customizations
{
    using AutoMocking.Core.Tests;
    using AutoMoq.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    [Collection("AutoMoqDataCustomization")]
    [Trait("Category", "Moq")]
    public class AutoMoqDataCustomizationTests
    {
        [Theory(DisplayName = "GIVEN existing customization for fixture WHEN Customize is invoked THEN fixture is appropriately customized")]
        [AutoData]
        public void GivenExistingCustomizationForFixture_WhenCustomizeIsInvoked_ThenFixtureIsAppropriatelyCustomized(Fixture fixture, AutoMoqDataCustomization customization)
        {
            // Arrange
            // Act
            fixture.Customize(customization);

            // Assert
            fixture.ShouldBeAutoMockingCustomized<MockPostprocessor>();
            fixture.ShouldNotThrowOnRecursion();
            fixture.ShouldOmitRecursion();
        }
    }
}
