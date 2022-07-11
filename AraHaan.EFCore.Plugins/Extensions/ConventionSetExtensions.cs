namespace Microsoft.EntityFrameworkCore;

internal static class ConventionSetExtensions
{
    internal static void AddExtendedConventionSet(this ConventionSet conventionSet)
    {
        var dateTimeKindAttributeConvention = new DateTimeKindAttributeConvention();
        conventionSet.EntityTypeAddedConventions.Add(dateTimeKindAttributeConvention);
        conventionSet.EntityTypeBaseTypeChangedConventions.Add(dateTimeKindAttributeConvention);
        conventionSet.ModelFinalizingConventions.Add(dateTimeKindAttributeConvention);
    }
}
