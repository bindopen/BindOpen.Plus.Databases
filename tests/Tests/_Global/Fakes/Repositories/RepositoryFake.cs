using BindOpen.Databases.Connectors;

namespace BindOpen.Databases.Tests.Fakes
{
    /// <summary>
    /// This class represents a test repository.
    /// </summary>
    public partial class RepositoryFake : TBdoDbRepository<BdoDbRelationalConnector, DbModelFake>
    {
        /// <summary>
        /// 
        /// </summary>
        public RepositoryFake()
        {
        }
    }
}
