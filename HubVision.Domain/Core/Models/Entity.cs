using CoreRun.Domain.Core.Models;

namespace CoreRun.Domain.Core.Models
{
    public abstract class Entity<TKey> where TKey : IEquatable<TKey>
    {
        public TKey Id { get; protected set; }

    }
}
