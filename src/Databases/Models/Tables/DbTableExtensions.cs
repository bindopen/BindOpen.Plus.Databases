using BindOpen.Data.Helpers;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class contains database field extensions.
    /// </summary>
    public static class DbTableExtensions
    {
        /// <summary>
        /// Gets the BindOpen script corresponding to the specified table.
        /// </summary>
        /// <param name="table">The table to consider.</param>
        /// <returns>The interpreted string value.</returns>
        public static string ToScript(this IDbTable table)
        {
            string st = "";

            if (table != null)
            {
                if (!string.IsNullOrEmpty(table.Alias))
                {
                    st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + table.Alias + "')";
                }
                else
                {
                    st = st.ConcatenateIf(!string.IsNullOrEmpty(table.DataModule), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlDatabase('" + table.DataModule + "').");

                    st = st.ConcatenateIf(!string.IsNullOrEmpty(table.Schema), st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlSchema('" + table.Schema + "').");

                    if (!string.IsNullOrEmpty(table.Name))
                    {
                        st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('" + table.Name + "')";
                    }
                    else
                    {
                        st = st.ConcatenateIf(string.IsNullOrEmpty(st), "$") + "sqlTable('<!-TABLE_MISSING-!>')";
                    }
                }
            }

            return st;
        }
    }
}