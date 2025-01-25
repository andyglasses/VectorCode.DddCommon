using System.Collections.Immutable;

namespace VectorCode.DddCommon;

/// <summary>
/// Base class for Aggregate Root, based on the Base Entity.  You only need to use if you wish to use version-ing and domain events
/// </summary>
/// <typeparam name="TIdentity"></typeparam>
/// <typeparam name="TDto"></typeparam>
/// <typeparam name="TVersion"></typeparam>
public abstract class AggregateRoot<TIdentity, TDto, TVersion> : Entity<TIdentity, TDto>
  where TIdentity : notnull
{
  /// <summary>
  /// Version of the Aggregate Root
  /// </summary>
  public TVersion Version { get; protected set; }

  private List<IDomainEvent> _domainEvents = new List<IDomainEvent>();

  /// <summary>
  /// Raise a domain event
  /// </summary>
  /// <param name="domainEvent"></param>
  protected void AddEvent(IDomainEvent domainEvent)
  {
    _domainEvents.Add(domainEvent);
  }

  /// <summary>
  /// Get the list of domain events
  /// </summary>
  public void ClearEvents()
  {
    _domainEvents.Clear();
  }

  /// <summary>
  /// Get the list of domain events
  /// </summary>
  /// <returns></returns>
  public IImmutableList<IDomainEvent> GetEvents()
  {
    return _domainEvents.ToImmutableList();
  }



  /// <summary>
  /// Create a new instance of the Aggregate Root
  /// </summary>
  /// <param name="id"></param>
  /// <param name="version"></param>
  public AggregateRoot(TIdentity id, TVersion version) : base(id)
  {
    Version = version;
  }
}
