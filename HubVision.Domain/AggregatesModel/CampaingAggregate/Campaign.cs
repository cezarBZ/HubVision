using HubVision.Domain.AggregatesModel.AdAccountAggregate;
using HubVision.Domain.AggregatesModel.AdSetAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.CampaingAggregate
{
    public class Campaign : TenantEntity<Guid>, IAggregateRoot
    {
        public string Name { get; set; }
        public string Objective { get; set; }
        public string Status { get; set; }
        public decimal? DailyBudget { get; set; }
        public decimal? LifetimeBudget { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public Guid AdAccountId { get; set; }
        public AdAccount AdAccount { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<AdSet> AdSets { get; set; }

        private Campaign()
        {
            AdSets = new List<AdSet>();
        }
    }
}
