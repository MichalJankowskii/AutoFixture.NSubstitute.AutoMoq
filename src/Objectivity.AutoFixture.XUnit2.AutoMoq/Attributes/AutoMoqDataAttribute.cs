namespace Objectivity.AutoFixture.XUnit2.AutoMoq.Attributes
{
    using System;
    using AutoMocking.Core.Attributes;
    using AutoMocking.Core.Customizations;
    using AutoMocking.Core.Providers;
    using Customizations;
    using Ploeh.AutoFixture;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AutoMoqDataAttribute : AutoMockingDataAttribute
    {
        public AutoMoqDataAttribute()
        {
        }

        public AutoMoqDataAttribute(IFixture fixture, IAutoFixtureAttributeProvider provider)
            : base(fixture, provider)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoMoqDataCustomization();
        }
    }
}