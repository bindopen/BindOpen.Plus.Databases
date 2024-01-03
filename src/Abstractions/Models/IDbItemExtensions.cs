using BindOpen.Data;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents an extension of the IDbFieldExtension enumeration.
    /// </summary>
    public static class IDbItemExtensions
    {
        public static IDbField WithExpression(this IDbField field, IBdoExpression exp)
        {
            if (field != null)
            {
                field.Expression = exp;
            }

            return field;
        }
    }
}
