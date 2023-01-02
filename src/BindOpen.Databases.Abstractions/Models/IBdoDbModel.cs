using BindOpen.Data;
using BindOpen.Data.Items;

namespace BindOpen.Databases.Models
{
    public partial interface IBdoDbModel :
        ITIdentifiedPoco<IBdoDbModel>, IReferenced
    {
    }
}