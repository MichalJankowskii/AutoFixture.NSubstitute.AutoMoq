namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests.Helpers
{
    using System;
    using Core.Attributes;
    using Core.Customizations;
    using Core.Providers;
    using Ploeh.AutoFixture;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InlineAutoMockingTestHelperDataAttribute : InlineAutoMockingDataAttribute
    {
        public InlineAutoMockingTestHelperDataAttribute(params object[] values)
            : base(values)
        {
        }

        public InlineAutoMockingTestHelperDataAttribute(IFixture fixture, IAutoFixtureInlineAttributeProvider provider, params object[] values)
            : base(fixture, provider, values)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoMockingTestHelperDataCustomization();
        }
    }
}