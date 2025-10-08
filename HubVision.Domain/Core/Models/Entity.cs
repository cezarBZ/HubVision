using HubVision.Domain.Core.Models;

namespace HubVision.Domain.Core.Models
{
    public abstract class Entity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; protected set; }

    }
}
