using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.ClientAggregate
{
    public class ClientAssignment : TenantEntity<Guid>
    {
        public Guid ClientId { get; set; }
        public Client Client { get; set; }

        public Guid TrafficManagerId { get; set; }
        public TrafficManager TrafficManager { get; set; }

        public bool IsPrimary { get; set; }
        public DateTime AssignedAt { get; set; }
        public Guid AssignedBy { get; set; }
        public DateTime? RemovedAt { get; set; }

        public static ClientAssignment Create(
            Guid agencyId,
            Guid clientId,
            Guid trafficManagerId,
            bool isPrimary,
            Guid assignedBy)
        {
            return new ClientAssignment
            {
                AgencyId = agencyId,
                ClientId = clientId,
                TrafficManagerId = trafficManagerId,
                IsPrimary = isPrimary,
                AssignedAt = DateTime.UtcNow,
                AssignedBy = assignedBy
            };
        }
    }

}
