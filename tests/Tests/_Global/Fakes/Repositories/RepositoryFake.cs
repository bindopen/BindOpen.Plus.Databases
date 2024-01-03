using BindOpen.Databases.Models;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a test repository.
    /// </summary>
    public partial class RepositoryFake : TBdoDbRepository<DbModelFake>
    {
        /// <summary>
        /// 
        /// </summary>
        public RepositoryFake()
        {
        }
    }
}
