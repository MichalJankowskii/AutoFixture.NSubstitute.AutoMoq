namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Attributes
{
    using System;
    using AutoMocking.Core.Attributes;
    using AutoMocking.Core.Customizations;
    using Customizations;
    using Ploeh.AutoFixture;
    using Xunit.Sdk;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [DataDiscoverer("Xunit.Sdk.MemberDataDiscoverer", "xunit.core")]
    public sealed class MemberAutoNSubstituteDataAttribute : MemberAutoMockingDataAttribute
    {
        public MemberAutoNSubstituteDataAttribute(string memberName, params object[] parameters)
            : base(memberName, parameters)
        {
        }

        public MemberAutoNSubstituteDataAttribute(IFixture fixture, string memberName, params object[] parameters)
            : base(fixture, memberName, parameters)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoNSubstituteDataCustomization();
        }
    }
}