using BindOpen.Data;
using System;

namespace BindOpen.Plus.Databases.Models
{
    /// <summary>
    /// This class represents a field attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class BdoDbFieldAttribute : Attribute
    {
        // ------------------------------------------
        // PROPERTIES
        // ------------------------------------------

        #region Properties

        /// <summary>
        /// The name of this instance.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value type of this instance.
        /// </summary>
        public DataValueTypes ValueType { get; set; }

        #endregion

        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbFieldAttribute class.
        /// </summary>
        /// <param name="name">The name to consider.</param>
        /// <param name="valueType">The value type to consider.</param>
        public BdoDbFieldAttribute(
            string name,
            DataValueTypes valueType = DataValueTypes.Any) : base()
        {
            Name = name;
            ValueType = valueType;
        }

        #endregion
    }
}
