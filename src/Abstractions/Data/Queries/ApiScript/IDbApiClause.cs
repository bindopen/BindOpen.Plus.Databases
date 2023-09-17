﻿using BindOpen.Kernel.Data;

namespace BindOpen.Plus.Databases.Data
{
    /// <summary>
    /// This class represents a Api script expression.
    /// </summary>
    public interface IDbApiClause : IBdoObject
    {
        /// <summary>
        /// The field alias of this instance.
        /// </summary>
        public string FieldAlias { get; set; }

        /// <summary>
        /// The field of this instance.
        /// </summary>
        public IDbField Field { get; set; }
    }
}
