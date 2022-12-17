using BindOpen.Framework.MetaData;
using BindOpen.Framework.MetaData.Items;

namespace BindOpen.Databases.Models
{
    public partial interface IBdoDbModel :
        ITIdentifiedPoco<IBdoDbModel>, IReferenced
    {
    }
}