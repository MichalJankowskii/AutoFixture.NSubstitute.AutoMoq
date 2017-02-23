namespace Objectivity.AutoFixture.XUnit2.NSubstitute.Tests.Customizations
{
    using AutoNSubstitute.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Xunit2;
    using Xunit;

    [Collection("OmitOnRecursionCustomization")]
    [Trait("Category", "Customizations")]
    public class OmitOnRecursionCustomizationTests
    {
        [Theory(DisplayName = "GIVEN existing customization for fixture WHEN Customize is invoked THEN fixture should omit recursion")]
        [AutoData]
        public void GivenExistingCustomizationForFixture_WhenCustomizeIsInvoked_ThenFixtureShouldOmitRecursion(Fixture fixture, OmitOnRecursionCustomization customization)
        {
            // Arrange
            // Act
            fixture.Customize(customization);

            // Assert
            fixture.ShouldOmitRecursion();
        }
    }
}