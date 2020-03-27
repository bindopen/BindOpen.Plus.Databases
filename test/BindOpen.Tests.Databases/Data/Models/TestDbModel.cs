using BindOpen.Data.Models;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Fidal.BrandManager.Tests.Infrastructure")]
namespace BindOpen.Tests.Databases.Data.Models
{
    /// <summary>
    /// This class represents a BrandManager database model.
    /// </summary>
    public partial class TestDbModel : BdoDbModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="builder"></param>
        public override void OnCreating(IBdoDbModelBuilder builder)
        {
            OnCreating_Test1(builder);
            OnCreating_Test2(builder);
        }
    }
}
