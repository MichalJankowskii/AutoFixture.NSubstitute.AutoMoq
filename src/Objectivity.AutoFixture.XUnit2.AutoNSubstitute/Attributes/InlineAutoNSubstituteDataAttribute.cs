namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Attributes
{
    using System;
    using AutoMocking.Core.Attributes;
    using AutoMocking.Core.Customizations;
    using AutoMocking.Core.Providers;
    using Customizations;
    using Ploeh.AutoFixture;

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1019:DefineAccessorsForAttributeArguments", Justification = "Parameter 'values' is exposed with ReadOnlyCollection.")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public sealed class InlineAutoNSubstituteDataAttribute : InlineAutoMockingDataAttribute
    {
        public InlineAutoNSubstituteDataAttribute(params object[] values)
            : base(values)
        {
        }

        public InlineAutoNSubstituteDataAttribute(IFixture fixture, IAutoFixtureInlineAttributeProvider provider, params object[] values)
            : base(fixture, provider, values)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoNSubstituteDataCustomization();
        }
    }
}