namespace Microsoft.EntityFrameworkCore.Metadata.Builders;

/// <summary>
/// Extensions for <see cref="IConventionEntityTypeBuilder" /> and <see cref="IConventionPropertyBuilder" /> for non-relational databases.
/// </summary>
public static class ConventionBuilderExtensions
{
    /// <summary>
    ///     Configures the <see cref="DateTimeKind" /> for datetime properties on the entity.
    /// </summary>
    /// <param name="conventionEntityTypeBuilder">
    ///     The convension entity type builder.
    /// </param>
    /// <param name="kind">
    ///     The <see cref="DateTimeKind"/> value to use with the internal <see cref="ValueConverter" />.
    /// </param>
    /// <param name="includeNullable">
    ///     Determines if a nullable <see cref="ValueConverter" /> should be added internally.
    /// </param>
    /// <param name="includeNonNullable">
    ///     Determines if a non-nullable <see cref="ValueConverter" /> should be added internally.
    /// </param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionEntityTypeBuilder? HasDateTimeKind(
        this IConventionEntityTypeBuilder conventionEntityTypeBuilder,
        DateTimeKind kind,
        bool includeNullable = true,
        bool includeNonNullable = true,
        bool fromDataAnnotation = false)
    {
        foreach (var property in conventionEntityTypeBuilder.Metadata.GetProperties())
        {
            if (property.ClrType.Equals(typeof(DateTime)))
            {
                _ = property.Builder.HasDateTimeKind(
                    kind,
                    includeNullable,
                    includeNonNullable,
                    fromDataAnnotation);
            }
        }

        return conventionEntityTypeBuilder;
    }

    /// <summary>
    ///     Configures the <see cref="DateTimeKind" /> for this datetime property.
    /// </summary>
    /// <param name="conventionPropertyBuilder">
    ///     The convension property builder.
    /// </param>
    /// <param name="kind">
    ///     The <see cref="DateTimeKind"/> value to use with the internal <see cref="ValueConverter" />.
    /// </param>
    /// <param name="includeNullable">
    ///     Determines if a nullable <see cref="ValueConverter" /> should be added internally.
    /// </param>
    /// <param name="includeNonNullable">
    ///     Determines if a non-nullable <see cref="ValueConverter" /> should be added internally.
    /// </param>
    /// <param name="fromDataAnnotation">Indicates whether the configuration was specified using a data annotation.</param>
    /// <returns>
    ///     The same builder instance if the configuration was applied, <see langword="null" /> otherwise.
    /// </returns>
    public static IConventionPropertyBuilder? HasDateTimeKind(
        this IConventionPropertyBuilder conventionPropertyBuilder,
        DateTimeKind kind,
        bool includeNullable = true,
        bool includeNonNullable = true,
        bool fromDataAnnotation = false)
    {
        var name = conventionPropertyBuilder.Metadata.Name;
        if (includeNonNullable)
        {
            _ = conventionPropertyBuilder.HasConversion(new ValueConverter<DateTime, DateTime>(
                fromCode => FromCodeToData(fromCode, name, kind),
                fromData => FromDataToCode(fromData, kind)), fromDataAnnotation);
        }

        if (includeNullable)
        {
            _ = conventionPropertyBuilder.HasConversion(new ValueConverter<DateTime?, DateTime?>(
                fromCode => fromCode != null ? FromCodeToData(fromCode.Value, name, kind) : default,
                fromData => fromData != null ? FromDataToCode(fromData.Value, kind) : default), fromDataAnnotation);
        }

        return conventionPropertyBuilder;
    }

    private static DateTime FromCodeToData(DateTime fromCode, string name, DateTimeKind kind)
        => fromCode.Kind.Equals(kind) switch
        {
            true => fromCode,
            false => throw new InvalidOperationException($"Column {name} only accepts {kind} date-time values"),
        };

    private static DateTime FromDataToCode(DateTime fromData, DateTimeKind kind)
        => (fromData.Kind, kind) switch
        {
            (DateTimeKind.Unspecified, not DateTimeKind.Unspecified) => DateTime.SpecifyKind(fromData, kind),
            (DateTimeKind.Local, DateTimeKind.Utc) => fromData.ToUniversalTime(),
            (DateTimeKind.Utc, DateTimeKind.Local) => fromData.ToLocalTime(),
            _ => fromData,
        };
}
