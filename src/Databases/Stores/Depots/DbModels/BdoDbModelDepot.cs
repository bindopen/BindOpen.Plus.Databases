using BindOpen.Data.Assemblies;
using BindOpen.Data.Stores;
using BindOpen.Logging;
using BindOpen.Plus.Databases.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Plus.Databases.Stores
{
    /// <summary>
    /// This class represents a database model depot.
    /// </summary>
    public class BdoDbModelDepot : TBdoDepot<IBdoDbModel>, IBdoDbModelDepot
    {
        // ------------------------------------------
        // CONSTRUCTORS
        // ------------------------------------------

        #region Constructors

        /// <summary>
        /// Instantiates a new instance of the BdoDbModelDepot class.
        /// </summary>
        public BdoDbModelDepot() : base()
        {
            Id = "dbModels";
        }

        #endregion

        // ------------------------------------------
        // IBdoDbModelDepot Implementation
        // ------------------------------------------

        #region IBdoDbModelDepot

        /// <summary>
        /// Queries of this instance.
        /// </summary>
        public List<IBdoDbModel> Models
        {
            get => Items?.ToList();
        }

        /// <summary>
        /// Adds a new item.
        /// </summary>
        /// <param name="item">The new item to add.</param>
        /// <returns>Returns the new item that has been added.
        /// Returns null if the new item is null or else its name is null.</returns>
        /// <remarks>The new item must have a name.</remarks>
        public override IBdoDbModel Insert(IBdoDbModel item)
        {
            if (item != null)
            {
                if (string.IsNullOrEmpty(item.Key()))
                {
                    item.Id = item.GetType().Name;
                }
            }

            return base.Insert(item);
        }

        /// <summary>
        /// Add the items from the specified assembly.
        /// </summary>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="log">The log to consider.</param>
        public override IBdoDepot AddFromAssembly(IBdoAssemblyReference reference, IBdoLog log = null)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAssembly(reference);
            if (assembly != null)
            {
                var types = assembly.GetTypes().Where(p => p.IsClass && typeof(IBdoDbModel).IsAssignableFrom(p));
                foreach (Type type in types)
                {
                    var model = Activator.CreateInstance(type) as BdoDbModel;

                    this.Insert(model);
                }
            }

            return this;
        }

        #endregion
    }
}
