using BindOpen.Databases.Data.Repositories;
using BindOpen.Tests.Databases.PostgreSql.Data.Models;

namespace BindOpen.Tests.Databases.PostgreSql.Data.Repositories
{
    /// <summary>
    /// This class represents a test repository.
    /// </summary>
    public partial class TestRepository : TBdoDbRepository<TestDbModel>
    {
        /// <summary>
        /// 
        /// </summary>
        public TestRepository()
        {
        }
    }
}
