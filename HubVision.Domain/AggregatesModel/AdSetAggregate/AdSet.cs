using HubVision.Domain.AggregatesModel.AdAggregate;
using HubVision.Domain.AggregatesModel.CampaingAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.AdSetAggregate
{
    public class AdSet : TenantEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public decimal DailyBudget { get; set; }
        public decimal? LifetimeBudget { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public string TargetingJson { get; set; }
        public string OptimizationGoal { get; set; }
        public string BillingEvent { get; set; }

        public Guid CampaignId { get; set; }
        public Campaign Campaign { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<Ad> Ads { get; set; }

        private AdSet()
        {
            Ads = new List<Ad>();
        }
    }
}
