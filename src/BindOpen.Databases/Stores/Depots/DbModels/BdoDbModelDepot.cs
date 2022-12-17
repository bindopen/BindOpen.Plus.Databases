using BindOpen.Databases.Models;
using BindOpen.Framework.MetaData.Stores;
using BindOpen.Framework.Runtime.Assemblies;
using BindOpen.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BindOpen.Databases.Stores
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
            get => Items;
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
        public override IBdoDepot AddFromAssembly(string assemblyName, IBdoLog log = null)
        {
            Assembly assembly = AppDomain.CurrentDomain.GetAsssembly(assemblyName);
            if (assembly != null)
            {
                var types = assembly.GetTypes().Where(p => p.IsClass && typeof(IBdoDbModel).IsAssignableFrom(p));
                foreach (Type type in types)
                {
                    var model = Activator.CreateInstance(type) as BdoDbModel;

                    Add(model);
                }
            }

            return this;
        }

        /// <summary>
        /// Gets the database model of the specified type and with the specified name.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        public override BdoDbModel Get<BdoDbModel>(string key = null)
        {
            var item = Items?.FirstOrDefault(p => p is BdoDbModel);
            return item as BdoDbModel;
        }

        #endregion
    }
}
