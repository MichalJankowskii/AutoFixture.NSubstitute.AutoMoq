namespace Objectivity.AutoFixture.XUnit2.AutoMoq.Customizations
{
    using AutoMocking.Core.Common;
    using AutoMocking.Core.Customizations;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.AutoMoq;

    public class AutoMoqDataCustomization : IAutoMockingDataCustomization
    {
        public void Customize(IFixture fixture)
        {
            fixture.NotNull(nameof(fixture))
                .Customize(new AutoConfiguredMoqCustomization())
                .Customize(new DoNotThrowOnRecursionCustomization())
                .Customize(new OmitOnRecursionCustomization());
        }
    }
}
