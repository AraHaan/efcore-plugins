namespace Microsoft.EntityFrameworkCore.Metadata.Conventions;

/// <summary>
///     A convention that configures database datetimes based on the <see cref="DateTimeKindAttribute" />.
/// </summary>
/// <remarks>
///     See <see href="https://aka.ms/efcore-docs-conventions">Model building conventions</see> for more information and examples.
/// </remarks>
internal class DateTimeKindAttributeConvention : IEntityTypeAddedConvention, IEntityTypeBaseTypeChangedConvention, IModelFinalizingConvention
{
    /// <inheritdoc />
    public void ProcessEntityTypeAdded(
        IConventionEntityTypeBuilder entityTypeBuilder,
        IConventionContext<IConventionEntityTypeBuilder> context)
        => CheckDateTimeKindAttribute(entityTypeBuilder.Metadata);

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

        CheckDateTimeKindAttribute(entityTypeBuilder.Metadata);
    }

    /// <inheritdoc />
    public void ProcessModelFinalizing(
        IConventionModelBuilder modelBuilder,
        IConventionContext<IConventionModelBuilder> context)
    {
        foreach (var entityType in modelBuilder.Metadata.GetEntityTypes())
        {
            CheckDateTimeKindAttribute(entityType);
        }
    }

    private static void CheckDateTimeKindAttribute(IConventionEntityType entityType)
    {
        // if entity type has the attribute use that, else fall back to
        // ones defined in each property, however if defined also on property the
        // property version *should* then override this.
        var attribute = entityType.ClrType.GetCustomAttribute<DateTimeKindAttribute>();
        if (attribute is not null)
        {
            _ = entityType.Builder.HasDateTimeKind(
                attribute.DateTimeKind,
                fromDataAnnotation: true);
        }

        // check for the attribute on each property, if one is found set the internal
        // valueconverter for that property to use the user provided DateTimeKind value.
        foreach (var property in entityType.GetProperties())
        {
            attribute = property.ClrType.GetCustomAttribute<DateTimeKindAttribute>();
            if (attribute is not null)
            {
                var propertyBuilder = entityType.Builder.Property(
                    property.ClrType,
                    property.Name,
                    fromDataAnnotation: true);
                if (propertyBuilder is not null)
                {
                    _ = propertyBuilder.HasDateTimeKind(
                        attribute.DateTimeKind,
                        includeNullable: attribute.IsNullable,
                        includeNonNullable: !attribute.IsNullable,
                        fromDataAnnotation: true);
                }
            }
        }
    }
}
