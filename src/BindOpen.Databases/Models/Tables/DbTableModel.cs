﻿using BindOpen.Databases.Data;
using BindOpen.Data.Items;
using System.Collections.Generic;

namespace BindOpen.Databases.Models
{
    /// <summary>
    /// This class represents the table model.
    /// </summary>
    public class DbTableModel : BdoItem, IDbTableModel
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The table of this instance.
        /// </summary>
        public IDbTable Table { get; set; }

        /// <summary>
        /// The fields of this instance.
        /// </summary>
        public List<IDbField> Fields { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbTableModel class.
        /// </summary>
        public DbTableModel()
        {
        }

        #endregion
    }
}