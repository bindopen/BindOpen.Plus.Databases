﻿using BindOpen.Plus.Databases.Models;
using BindOpen.Data;
using System.Collections.Generic;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents the table model.
    /// </summary>
    public interface IDbTableModel : IBdoObject
    {
        /// <summary>
        /// The table of this instance.
        /// </summary>
        public IDbTable Table { get; set; }

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }
    }
}