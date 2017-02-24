namespace Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes
{
    using System;
    using AutoMocking.Core.Attributes;
    using AutoMocking.Core.Customizations;
    using AutoMocking.Core.Providers;
    using Customizations;
    using Ploeh.AutoFixture;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InlineAutoMoqDataAttribute : InlineAutoMockingDataAttribute
    {
        public InlineAutoMoqDataAttribute(params object[] values)
            : base(values)
        {
        }

        public InlineAutoMoqDataAttribute(IFixture fixture, IAutoFixtureInlineAttributeProvider provider, params object[] values)
            : base(fixture, provider, values)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoMoqDataCustomization();
        }
    }
}