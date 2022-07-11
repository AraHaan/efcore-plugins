namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

internal class ExtensionsConvensionSetPlugin : IConventionSetPlugin
{
    public ConventionSet ModifyConventions(ConventionSet conventionSet)
    {
        conventionSet.AddExtendedConventionSet();
        return conventionSet;
    }
}
