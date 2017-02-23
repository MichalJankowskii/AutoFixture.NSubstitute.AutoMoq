namespace Objectivity.AutoFixture.XUnit2.AutoNSubstitute.Providers
{
    using Ploeh.AutoFixture;
    using Xunit.Sdk;

    public interface IAutoFixtureAttributeProvider
    {
        DataAttribute GetAttribute(IFixture fixture);
    }
}