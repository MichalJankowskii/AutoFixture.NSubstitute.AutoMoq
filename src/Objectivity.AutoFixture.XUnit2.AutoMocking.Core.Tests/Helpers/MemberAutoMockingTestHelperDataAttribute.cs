namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests.Helpers
{
    using System;
    using Core.Attributes;
    using Core.Customizations;
    using Ploeh.AutoFixture;
    using Xunit.Sdk;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    [DataDiscoverer("Xunit.Sdk.MemberDataDiscoverer", "xunit.core")]
    public sealed class MemberAutoMockingTestHelperDataAttribute : MemberAutoMockingDataAttribute
    {
        public MemberAutoMockingTestHelperDataAttribute(string memberName, params object[] parameters)
            : base(memberName, parameters)
        {
        }

        public MemberAutoMockingTestHelperDataAttribute(IFixture fixture, string memberName, params object[] parameters)
            : base(fixture, memberName, parameters)
        {
        }

        protected override IAutoMockingDataCustomization GenerateAutoMockingDataCustomization()
        {
            return new AutoMockingTestHelperDataCustomization();
        }
    }
}