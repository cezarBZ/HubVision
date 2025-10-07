namespace CoreRun.Domain.Core.Models
{
    public abstract class AggregateRoot<TKey> : Entity<TKey> where TKey : IEquatable<TKey>
    {

    }
}
