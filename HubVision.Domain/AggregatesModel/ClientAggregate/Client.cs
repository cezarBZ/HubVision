using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.ClientAggregate
{
    public class Client : TenantEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Phone { get; set; }
        public string PhotoUrl { get; set; }
        public string Company { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }

        public ICollection<AdAccount> AdAccounts { get; set; }
        public ICollection<ClientAssignment> ClientAssignments { get; set; }

        private Client()
        {
            AdAccounts = new List<AdAccount>();
            ClientAssignments = new List<ClientAssignment>();
        }

        public static Client Create(
            Guid agencyId,
            string name,
            string email,
            string passwordHash)
        {
            return new Client
            {
                Id = Guid.NewGuid(),
                AgencyId = agencyId,
                Name = name,
                Email = email,
                PasswordHash = passwordHash,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
