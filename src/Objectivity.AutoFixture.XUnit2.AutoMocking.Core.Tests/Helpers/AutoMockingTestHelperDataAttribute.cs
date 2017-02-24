namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests.Helpers
{
    using System;
    using Core.Attributes;
    using Core.Customizations;
    using Core.Providers;
    using Ploeh.AutoFixture;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AutoMockingTestHelperDataAttribute : AutoMockingDataAttribute
    {
        public AutoMockingTestHelperDataAttribute()
        {
        }

        public AutoMockingTestHelperDataAttribute(IFixture fixture, IAutoFixtureAttributeProvider provider)
            : base(fixture, provider)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoMockingTestHelperDataCustomization();
        }
    }
}