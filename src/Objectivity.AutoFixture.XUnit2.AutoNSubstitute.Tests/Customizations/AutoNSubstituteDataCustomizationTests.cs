namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Tests.Customizations
{
    using AutoMocking.Core.Tests;
    using AutoNSubstitute.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    [Collection("AutoNSubstituteDataCustomization")]
    [Trait("Category", "NSubstitute")]
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
            fixture.ShouldBeAutoMockingCustomized<SubstituteRequestHandler>();
            fixture.ShouldNotThrowOnRecursion();
            fixture.ShouldOmitRecursion();
        }
    }
}
