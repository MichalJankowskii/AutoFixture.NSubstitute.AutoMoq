namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Attributes
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Common;
    using Customizations;
    using Ploeh.AutoFixture;
    using Providers;
    using Xunit.Sdk;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public abstract class AutoMockingDataAttribute : DataAttribute
    {
        protected AutoMockingDataAttribute()
            : this(new Fixture(), new AutoDataAttributeProvider())
        {
        }

        protected AutoMockingDataAttribute(IFixture fixture, IAutoFixtureAttributeProvider provider)
        {
            this.Fixture = fixture.NotNull(nameof(fixture));
            this.Provider = provider.NotNull(nameof(provider));
        }

        public IFixture Fixture { get; }

        /// <summary>
        /// Gets or sets a value indicating whether virtual members should be ignored during object creation.
        /// </summary>
        public bool IgnoreVirtualMembers { get; set; } = false;

        public IAutoFixtureAttributeProvider Provider { get; }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            this.Fixture.Customize(this.GenerateAutoMockingDataCustomization());
            this.Fixture.Customize(new IgnoreVirtualMembersCustomization(this.IgnoreVirtualMembers));

            return this.Provider.GetAttribute(this.Fixture).GetData(testMethod);
        }

        protected abstract IAutoMockingDataCustomization GenerateAutoMockingDataCustomization();
    }
}