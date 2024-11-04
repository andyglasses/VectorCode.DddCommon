namespace CodedVector.DddCommon;

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

    return Id.Equals(((BaseEntity<TIdentity, TDto>)obj).Id);
  }

  ///<inheritdoc/>
  public override int GetHashCode()
  {
    return Id.GetHashCode();
  }
}
