using System.Linq;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Alterations;
using FluentNHibernate.Conventions.Inspections;
using FluentNHibernate.MappingModel;
using NUnit.Framework;

namespace FluentNHibernate.Testing.ConventionsTests.AcceptanceCriteria
{
    [TestFixture]
    public class PropertyAcceptanceCriteriaInheritanceTests
    {
        private IAcceptanceCriteria<IPropertyInspector> acceptance;

        [SetUp]
        public void CreateAcceptanceCriteria()
        {
            acceptance = new ConcreteAcceptanceCriteria<IPropertyInspector>();
        }

        [Test]
        public void ShouldInheritCriteriaFromAnotherConvention()
        {
            acceptance
                .SameAs<AnotherConvention>();

            acceptance.Expectations.Count()
                .ShouldEqual(2);
        }

        [Test]
        public void ShouldUseInheritedCriteria()
        {
            acceptance
                .SameAs<AnotherConvention>();

            acceptance.Matches(new PropertyInspector(new PropertyMapping
            {
                Insert = false,
                Update = false
            }))
                .ShouldBeTrue();
        }

        [Test]
        public void ShouldInheritCriteriaFromAnotherConventionOpposite()
        {
            acceptance
                .OppositeOf<AnotherConvention>();

            acceptance.Expectations.Count()
                .ShouldEqual(2);
        }

        [Test]
        public void ShouldUseInheritedOppositeCriteria()
        {
            acceptance
                .OppositeOf<AnotherConvention>();

            acceptance.Matches(new PropertyInspector(new PropertyMapping
            {
                Insert = true,
                Update = true
            }))
                .ShouldBeTrue();
        }

        private class AnotherConvention : IPropertyConvention
        {
            public void Accept(IAcceptanceCriteria<IPropertyInspector> acceptance)
            {
                acceptance
                    .Expect(x => x.Insert == false)
                    .Expect(x => x.Update == false);
            }

            public void Apply(IPropertyAlteration alteration, IPropertyInspector inspector)
            { }
        }
    }
}