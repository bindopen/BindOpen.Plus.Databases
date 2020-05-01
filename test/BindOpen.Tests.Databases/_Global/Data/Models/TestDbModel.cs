using BindOpen.Databases.Data.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fidal.BrandManager.Tests.Infrastructure")]
namespace BindOpen.Tests.Databases.PostgreSql.Data.Models
{
    /// <summary>
    /// This class represents a BrandManager database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
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
