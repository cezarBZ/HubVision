using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.AdAccountAggregate
{
    public class AdAccountAccess : TenantEntity<Guid>
    {
        public Guid AdAccountId { get; set; }
        public AdAccount AdAccount { get; set; }

        public Guid TrafficManagerId { get; set; }
        public TrafficManager TrafficManager { get; set; }

        public AccessLevel AccessLevel { get; set; }
        public DateTime GrantedAt { get; set; }
        public Guid GrantedBy { get; set; }

        public static AdAccountAccess Create(
            Guid agencyId,
            Guid adAccountId,
            Guid trafficManagerId,
            AccessLevel accessLevel,
            Guid grantedBy)
        {
            return new AdAccountAccess
            {
                Id = Guid.NewGuid(),
                AgencyId = agencyId,
                AdAccountId = adAccountId,
                TrafficManagerId = trafficManagerId,
                AccessLevel = accessLevel,
                GrantedAt = DateTime.UtcNow,
                GrantedBy = grantedBy
            };
        }
    }
}
