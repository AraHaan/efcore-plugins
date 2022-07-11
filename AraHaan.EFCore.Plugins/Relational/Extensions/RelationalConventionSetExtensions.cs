namespace Microsoft.EntityFrameworkCore;

internal static class RelationalConventionSetExtensions
{
    internal static void AddExtendedRelationalConventionSet(this ConventionSet conventionSet)
    {
        conventionSet.AddExtendedConventionSet();
        var defaultValueSqlAttributeConvention = new DefaultValueSqlAttributeConvention();
        conventionSet.EntityTypeAddedConventions.Add(defaultValueSqlAttributeConvention);
        conventionSet.EntityTypeBaseTypeChangedConventions.Add(defaultValueSqlAttributeConvention);
        conventionSet.ModelFinalizingConventions.Add(defaultValueSqlAttributeConvention);
    }
}
