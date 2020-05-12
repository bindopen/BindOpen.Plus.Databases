using System;
using System.Xml.Serialization;

namespace BindOpen.Databases.Data.Queries
{
    /// <summary>
    /// This enumerates the possible modes of database query table.
    /// </summary>
    [Serializable()]
    [XmlType("DbQueryTableMode", Namespace = "https://docs.bindopen.org/xsd")]
    public enum DbQueryTableMode
    {
        /// <summary>
        /// Only name.
        /// </summary>
        OnlyName,

        /// <summary>
        /// Complete name.
        /// </summary>
        CompleteName,

        /// <summary>
        /// Complete name as alias.
        /// </summary>
        CompleteNameAsAlias,

        /// <summary>
        /// Only name as alias.
        /// </summary>
        OnlyNameAsAlias,

        /// <summary>
        /// Only query.
        /// </summary>
        OnlyQuery,

        /// <summary>
        /// Alias as query.
        /// </summary>
        AliasAsQuery
    }
}
