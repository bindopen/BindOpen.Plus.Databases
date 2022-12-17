using BindOpen.Databases.Repositories;
using BindOpen.Databases.Tests.PostgreSql.Data.Models;

namespace BindOpen.Databases.Tests.PostgreSql.Data.Repositories
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
