namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Customizations
{
    using Common;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoNSubstitute;

    public class AutoNSubstituteDataCustomization : ICustomization
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
