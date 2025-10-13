using HubVision.Domain.AggregatesModel.AdSetAggregate;
using HubVision.Domain.Core.Models;

namespace HubVision.Domain.AggregatesModel.AdAggregate
{
    public class Ad : TenantEntity<Guid>, IAggregateRoot
    {
        public required string Name { get; set; }
        public required string Status { get; set; }
        public required string CreativeId { get; set; }
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AdText { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public string? CallToAction { get; set; }
        public string? LinkUrl { get; set; }

        public Guid AdSetId { get; set; }
        public required AdSet AdSet { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
