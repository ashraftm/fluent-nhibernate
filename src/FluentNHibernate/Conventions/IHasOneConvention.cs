using FluentNHibernate.Mapping;

namespace FluentNHibernate.Conventions
{
    /// <summary>
    /// HasOne convention, used for applying changes to one-to-one relationships.
    /// </summary>
    public interface IHasOneConvention : IConvention<IOneToOnePart>
    {}
}