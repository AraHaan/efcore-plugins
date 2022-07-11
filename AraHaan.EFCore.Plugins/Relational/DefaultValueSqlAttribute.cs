namespace Microsoft.EntityFrameworkCore;

/// <summary>
///     Specifies that an property on an entity that has
///     a specific default sql value.
/// </summary>
/// <remarks>
///     <para>
///         This attribute can only be used on properties for which has
///         a specific default sql value.
///     </para>
///     <para>
///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
///         examples.
///     </para>
/// </remarks>
[AttributeUsage(AttributeTargets.Property)]
public class DefaultValueSqlAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DefaultValueSqlAttribute"/> class.
    /// </summary>
    /// <param name="defaultValueSql">The default sql value for the property.</param>
    public DefaultValueSqlAttribute(string defaultValueSql)
        => this.DefaultValueSql = defaultValueSql;

    /// <summary>
    /// Gets the specific default sql value for the property.
    /// </summary>
    public string DefaultValueSql { get; }
}
