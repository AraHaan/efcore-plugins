namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

internal class RelationalExtensionsConvensionSetPlugin : IConventionSetPlugin
{
    public ConventionSet ModifyConventions(ConventionSet conventionSet)
    {
        conventionSet.AddExtendedRelationalConventionSet();
        return conventionSet;
    }
}
