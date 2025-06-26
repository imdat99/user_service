namespace Account.Shared.Events;
public interface IDomainEvent
{
    DateTime OccurredOn { get; }
    Guid EventId { get; }
}