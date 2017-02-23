namespace Objectivity.AutoFixture.XUnit2.NSubstitute.Tests.Customizations
{
    using AutoNSubstitute.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    [Collection("AutoNSubstituteDataCustomization")]
    [Trait("Category", "Customizations")]
    public class AutoNSubstituteDataCustomizationTests
    {
        [Theory(DisplayName = "GIVEN existing customization for fixture WHEN Customize is invoked THEN fixture is appropriately customized")]
        [AutoData]
        public void GivenExistingCustomizationForFixture_WhenCustomizeIsInvoked_ThenFixtureIsAppropriatelyCustomized(Fixture fixture, AutoNSubstituteDataCustomization customization)
        {
            // Arrange
            // Act
            fixture.Customize(customization);

            // Assert
            fixture.ShouldBeAutoNSubstituteCustomized();
            fixture.ShouldNotThrowOnRecursion();
            fixture.ShouldOmitRecursion();
        }
    }
}
