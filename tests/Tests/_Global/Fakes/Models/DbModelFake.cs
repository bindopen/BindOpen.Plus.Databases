using BindOpen.Databases.Relational;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a BrandManager database model.
    /// </summary>
    public partial class DbModelFake : BdoDbRelationalModel
    {
        /// <summary>
        /// 
        /// </summary>
        public override void OnCreating()
        {
            OnCreating_Test1();
            OnCreating_Test2();
        }
    }
}
