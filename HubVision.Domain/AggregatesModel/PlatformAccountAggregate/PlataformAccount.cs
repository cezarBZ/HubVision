using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.PlatformAccountAggregate
{
    public class PlatformAccount : TenantEntity<Guid>, IAggregateRoot
    {
        public Guid TrafficManagerId { get; set; }
        public TrafficManager TrafficManager { get; set; }

        public PlatformType Platform { get; set; }
        public string PlatformUserId { get; set; }
        public string PlatformEmail { get; set; }
        public string DisplayName { get; set; }

        public string AccessToken { get; set; } 
        public string RefreshToken { get; set; }
        public DateTime? TokenExpiresAt { get; set; }

        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }
        public DateTime ConnectedAt { get; set; }
        public DateTime? LastSyncedAt { get; set; }

        public string PlatformMetadata { get; set; }

        // Relacionamentos
        public ICollection<AdAccount> AdAccounts { get; set; }

        private PlatformAccount()
        {
            AdAccounts = new List<AdAccount>();
        }

        public static PlatformAccount Create(
            Guid agencyId,
            Guid trafficManagerId,
            PlatformType platform,
            string platformUserId,
            string platformEmail,
            string displayName,
            string accessToken,
            string refreshToken,
            DateTime? tokenExpiresAt)
        {
            return new PlatformAccount
            {
                AgencyId = agencyId,
                TrafficManagerId = trafficManagerId,
                Platform = platform,
                PlatformUserId = platformUserId,
                PlatformEmail = platformEmail,
                DisplayName = displayName,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                TokenExpiresAt = tokenExpiresAt,
                IsActive = true,
                IsDefault = false,
                ConnectedAt = DateTime.UtcNow
            };
        }
    }
}
