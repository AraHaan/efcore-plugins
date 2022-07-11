namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Specifies that an entity that has datetime values or
///     an property on an entity that is an datetime value to
///     use a specific <see cref="System.DateTimeKind"/> configuration.
/// </summary>
/// <remarks>
///     <para>
///         This attribute can be used both on the property that returns an
///         datetime object and for the entity type that contains the
///         datetime objects. However if different values are specified for
///         both the one in the property would override the one specified on
///         the type (this is because properties are processed after the entity).
///     </para>
///     <para>
///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
///         examples.
///     </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
public class DateTimeKindAttribute : Attribute
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="DateTimeKindAttribute"/> class.
    /// </summary>
    public DateTimeKindAttribute()
        : this(DateTimeKind.Unspecified)
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="DateTimeKindAttribute"/> class.
    /// </summary>
    /// <param name="dateTimeKind">The <see cref="System.DateTimeKind"/> value to use for all datetimes.</param>
    public DateTimeKindAttribute(DateTimeKind dateTimeKind)
        => this.DateTimeKind = dateTimeKind;

    /// <summary>
    ///     Gets the <see cref="System.DateTimeKind"/> value set in this attribute instance.
    /// </summary>
    public DateTimeKind DateTimeKind { get; }

    /// <summary>
    ///     Gets or sets whether the current datetime object is nullable.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         The default value is false.
    ///     </para>
    ///     <para>
    ///         Note: This is ignored when the attribute is applied to the entity type
    ///         so that way both the nullable and non-nullable converters are added
    ///         internally for the case that a property might be nullable.
    ///     </para>
    /// </remarks>
    public bool IsNullable { get; set; } = false;
}
