using HubVision.Domain.AggregatesModel.AgencyAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.Core.Models
{
    public class TenantEntity<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey AgencyId { get; set; }
        public Agency Agency { get; set; }
    }
}
