using HubVision.Domain.AggregatesModel.CampaingAggregate;
using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.AggregatesModel.PlatformAccountAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.AdAccountAggregate;

public class AdAccount : TenantEntity<Guid>, IAggregateRoot
{
    public Guid PlatformAccountId { get; set; }
    public PlatformAccount PlatformAccount { get; set; }

    public string AccountName { get; set; }
    public string Currency { get; set; }
    public string TimeZone { get; set; }

    public AdAccountStatus Status { get; set; }
    public bool IsActive { get; set; }

    public Guid? ClientId { get; set; }
    public Client Client { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? LastSyncedAt { get; set; }

    public ICollection<Campaign> Campaigns { get; set; }
    public ICollection<AdAccountAccess> AdAccountAccesses { get; set; }

    private AdAccount()
    {
        Campaigns = new List<Campaign>();
        AdAccountAccesses = new List<AdAccountAccess>();
    }

    public static AdAccount Create(
        Guid agencyId,
        Guid platformAccountId,
        string accountName,
        string currency,
        string timeZone)
    {
        return new AdAccount
        {
            AgencyId = agencyId,
            PlatformAccountId = platformAccountId,
            AccountName = accountName,
            Currency = currency,
            TimeZone = timeZone,
            Status = AdAccountStatus.Active,
            IsActive = true,
            CreatedAt = DateTime.UtcNow
        };
    }
}
