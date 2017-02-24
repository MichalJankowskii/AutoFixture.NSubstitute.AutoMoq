namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Attributes
{
    using System;
    using AutoMocking.Core.Attributes;
    using AutoMocking.Core.Customizations;
    using AutoMocking.Core.Providers;
    using Customizations;
    using Ploeh.AutoFixture;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AutoNSubstituteDataAttribute : AutoMockingDataAttribute
    {
        public AutoNSubstituteDataAttribute()
        {
        }

        public AutoNSubstituteDataAttribute(IFixture fixture, IAutoFixtureAttributeProvider provider)
            : base(fixture, provider)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoNSubstituteDataCustomization();
        }
    }
}