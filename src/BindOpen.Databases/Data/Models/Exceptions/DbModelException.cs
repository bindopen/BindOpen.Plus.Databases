using System;

namespace BindOpen.Data.Models
{
    /// <summary>
    /// This class represents a database model expcetion.
    /// </summary>
    public class DbModelException : Exception
    {


        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the DbModelException class.
        /// </summary>
        /// <param name="message">The message to consider.</param>
        public DbModelException(string message) : base(message)
        {
        }

        #endregion
    }
}