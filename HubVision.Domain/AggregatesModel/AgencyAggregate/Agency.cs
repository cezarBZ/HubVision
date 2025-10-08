using HubVision.Domain.AggregatesModel.ClientAggregate;
using HubVision.Domain.AggregatesModel.TrafficManagerAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.AgencyAggregate;

public class Agency : Entity<Guid>, IAggregateRoot
{
    public string Name { get; set; }
    public string Slug { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public SubscriptionPlan Plan { get; set; }
    public string Phone { get; set; }
    public string LogoUrl { get; set; }
    public string Website { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? TrialEndsAt { get; set; }

    public ICollection<TrafficManager> TrafficManagers { get; set; }
    public ICollection<Client> Clients { get; set; }
    private Agency()
    {
        TrafficManagers = new List<TrafficManager>();
        Clients = new List<Client>();
    }

    public static Agency Create(string name, string slug, string email, SubscriptionPlan plan)
    {
        return new Agency
        {
            Id = Guid.NewGuid(),
            Name = name,
            Slug = slug,
            Email = email,
            Plan = plan,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            TrialEndsAt = DateTime.UtcNow.AddDays(30)
        };
    }
}
