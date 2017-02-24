namespace Objectivity.AutoFixture.XUnit2.AutoMocking.Core.Tests
{
    using System;
    using System.Linq.Expressions;
    using Core.SpecimenBuilders;
    using FluentAssertions;
    using Ploeh.AutoFixture;
    using Ploeh.AutoFixture.Kernel;

    public static class FixtureAssertionExtensions
    {
        public static void ShouldBeAutoMockingCustomized<TSpecimenBuilder>(this IFixture fixture) where TSpecimenBuilder : ISpecimenBuilder
        {
            Expression<Func<ISpecimenBuilder, bool>> mockProcessorPredicate =
                specimenBuilder =>
                    specimenBuilder is Postprocessor &&
                    ((Postprocessor) specimenBuilder).Builder is TSpecimenBuilder;

            // Ensure mock processor is added to customizations
            fixture.Customizations.Should().ContainSingle(mockProcessorPredicate);
        }

        public static void ShouldNotThrowOnRecursion(this IFixture fixture)
        {
            // Ensure there is no behaviour for throwing exception on recursive structures.
            fixture.Behaviors.Should().NotContain(b => b is ThrowingRecursionBehavior);
        }

        public static void ShouldOmitRecursion(this IFixture fixture)
        {
            // Ensure there is a beaviour added for omitting recursive types
            // on default recursion depth.
            fixture.Behaviors.Should().ContainSingle(b => b is OmitOnRecursionBehavior);
        }

        public static void ShouldNotIgnoreVirtualMembers(this IFixture fixture)
        {
            fixture.Customizations.Should().NotContain(s => s is IgnoreVirtualMembersSpecimenBuilder);
        }

        public static void ShouldIgnoreVirtualMembers(this IFixture fixture)
        {
            fixture.Customizations.Should().ContainSingle(s => s is IgnoreVirtualMembersSpecimenBuilder);
        }
    }
}