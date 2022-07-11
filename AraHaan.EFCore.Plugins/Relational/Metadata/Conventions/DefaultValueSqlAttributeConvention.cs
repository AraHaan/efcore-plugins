namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

/// <summary>
///     A convention that configures sql default values based on the <see cref="DefaultValueSqlAttribute" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> for more information and examples.
/// </remarks>
internal class DefaultValueSqlAttributeConvention : IEntityTypeAddedConvention, IEntityTypeBaseTypeChangedConvention, IModelFinalizingConvention
{
    /// <inheritdoc />
    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
        => CheckDefaultValueSqlAttribute(entityTypeBuilder.Metadata);

    /// <inheritdoc />
    public void ProcessEntityTypeBaseTypeChanged(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionEntityType? newBaseType,
        IConventionEntityType? oldBaseType,
        IConventionContext<IConventionEntityType> context)
    {
        if (oldBaseType is null)
        {
            return;
        }

        CheckDefaultValueSqlAttribute(entityTypeBuilder.Metadata);
    }

    /// <inheritdoc />
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            CheckDefaultValueSqlAttribute(entityType);
        }
    }

    private static void CheckDefaultValueSqlAttribute(IConventionEntityType entityType)
    {
        foreach (var property in entityType.GetProperties())
        {
            var attribute = property.ClrType.GetCustomAttribute<DefaultValueSqlAttribute>();
            if (attribute is not null)
            {
                var propertyBuilder = entityType.Builder.Property(
                    property.ClrType,
                    property.Name,
                    fromDataAnnotation: true);
                if (propertyBuilder is not null)
                {
                    propertyBuilder.HasDefaultValueSql(
                        attribute.DefaultValueSql,
                        fromDataAnnotation: true);
                }
            }
        }
    }
}
