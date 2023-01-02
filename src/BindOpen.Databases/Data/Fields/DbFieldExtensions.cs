using BindOpen.Data;

namespace BindOpen.Databases.Data
{
    /// <summary>
    /// This class contains database field extensions.
    /// </summary>
    public static class DbFieldExtensions
    {
        /// <summary>
        /// Gets the BindOpen script corresponding to the specified field.
        /// </summary>
        /// <param name="field">The field to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static string ToScript(this IDbField field)
        {
            string st = "";

            if (field != null)
            {
                if (!string.IsNullOrEmpty(field.DataTable)
                    || !string.IsNullOrEmpty(field.Schema)
                    || !string.IsNullOrEmpty(field.DataModule))
                {
                    st = DbFluent.Table(field.DataTable, field.Schema, field.DataModule).WithAlias(field.DataTableAlias).ToScript()
                        .ConcatenateIfFirstNotEmpty(".");
                }

                if (!string.IsNullOrEmpty(field.Alias))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + field.Alias + "')";
                }
                else if (!string.IsNullOrEmpty(field.Name))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('" + field.Name + "')";
                }
                else
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlField('<!-FIELD_MISSING-!>')";
                }
            }

            return st;
        }
    }
}