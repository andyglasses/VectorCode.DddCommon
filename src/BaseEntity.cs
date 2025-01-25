namespace VectorCode.DddCommon;

/// <summary>
/// Base Entity class
/// </summary>
/// <typeparam name="TIdentity">Identity field type</typeparam>
/// <typeparam name="TDto">Dto type</typeparam>
/// <param name="id">Identifier value</param>
public abstract class BaseEntity<TIdentity, TDto>(TIdentity id)
  where TIdentity : notnull
{
  /// <summary>
  /// Entity Identity
  /// </summary>
  public TIdentity Id { get; } = id;

  /// <summary>
  /// Generate a Dto for the entity
  /// </summary>
  /// <returns></returns>
  public abstract TDto ToDto();

  ///<inheritdoc/>
  public override bool Equals(object? obj)
  {
    if (obj == null || GetType() != obj.GetType())
    {
      return false;
    }

    if(Id.Equals(default(TIdentity)))
    {
      return false;
    }

    var other = obj as BaseEntity<TIdentity, TDto>;

    if(other!.Id.Equals(default (TIdentity)))
    {
      return false;
    }

    return Id.Equals(other.Id);
  }

  ///<inheritdoc/>
  public override int GetHashCode()
  {
    return (GetType().ToString() + Id).GetHashCode();
  }

  ///<inheritdoc/>
  public static bool operator ==(BaseEntity<TIdentity, TDto> a, BaseEntity<TIdentity, TDto> b)
  {
    if(ReferenceEquals(a, null) && ReferenceEquals(b, null))
    {
      return true;
    }
    if(ReferenceEquals(a, null) || ReferenceEquals(b, null))
    {
      return false;
    }
    return a.Equals(b);
  }

  ///<inheritdoc/>
  public static bool operator !=(BaseEntity<TIdentity, TDto> a, BaseEntity<TIdentity, TDto> b)
  {
    return !(a == b);
  }
}
