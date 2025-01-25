namespace VectorCode.DddCommon;

/// <summary>
/// Used to mark a class as a domain event handler
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1711:Identifiers should not have incorrect suffix", Justification = "Name matches purpose")]
public interface IDomainEventHandler<T> where T : IDomainEvent
{
  /// <summary>
  /// Handle a domain event
  /// </summary>
  /// <param name="domainEvent"></param>
  public void Handle(T domainEvent);
}
