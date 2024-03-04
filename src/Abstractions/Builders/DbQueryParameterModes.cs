using System;

namespace BindOpen.Databases.Relational.Builders
{
    /// <summary>
    /// This enumeration lists the possible modes of parameters.
    /// </summary>
    [Flags]
    public enum DbQueryParameterModes
    {
        /// <summary>
        /// Mode when parameters are scripted.
        /// </summary>
        Scripted = 0x01,

        /// <summary>
        /// Mode when values of parameters are injected.
        /// </summary>
        ValueInjected = 0x02,

        /// <summary>
        /// Mode when parameters are simply showed.
        /// </summary>
        SimplyShowed = 0x04,
    }

}
