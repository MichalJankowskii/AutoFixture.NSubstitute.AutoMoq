namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Customizations
{
    using AutoMocking.Core.Common;
    using AutoMocking.Core.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    public class AutoNSubstituteDataCustomization : IAutoMockingDataCustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.NotNull(nameof(fixture))
                .Customize(new AutoConfiguredNSubstituteCustomization())
                .Customize(new DoNotThrowOnRecursionCustomization())
                .Customize(new OmitOnRecursionCustomization());
        }
    }
}
