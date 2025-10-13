using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.TrafficManagerAggregate
{
    public class TrafficManager : TenantEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public TrafficManagerRole Role { get; set; }
        public bool IsActive { get; set; }
        public DateTime HiredAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<PlatformAccount> PlatformAccounts { get; set; }
        public ICollection<ClientAssignment> ClientAssignments { get; set; }
        public ICollection<AdAccountAccess> AdAccountAccesses { get; set; }


        private TrafficManager()
        {
            PlatformAccounts = new List<PlatformAccount>();
            ClientAssignments = new List<ClientAssignment>();
        }

        public static TrafficManager Create(
            Guid agencyId,
            string name,
            string email,
            string passwordHash,
            TrafficManagerRole role)
        {
            return new TrafficManager
            {
                AgencyId = agencyId,
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                Role = role,
                IsActive = true,
                HiredAt = DateTime.UtcNow
            };
        }
    }
}
