using System.Collections;
using System.Collections.Immutable;
using VectorCode.Common;

namespace VectorCode.DddCommon;

/// <summary>
/// Validation error collection, implements IImmutableList KeyCode
/// </summary>
public class ValidationErrorCollection : IImmutableList<KeyCode>
{
  private readonly ImmutableList<KeyCode> _validationErrors = new List<KeyCode>().ToImmutableList();

  /// <summary>
  /// Create an empty validation error collection
  /// </summary>
  public ValidationErrorCollection() { }

  /// <summary>
  /// Create a validation error collection from a list of errors
  /// </summary>
  /// <param name="errors"></param>
  public ValidationErrorCollection(IEnumerable<KeyCode> errors)
  {
    _validationErrors = errors.ToImmutableList();
  }

  ///<inheritdoc/>
  public KeyCode this[int index] => _validationErrors[index];
  ///<inheritdoc/>
  public int Count => _validationErrors.Count;

  ///<inheritdoc/>
  public IImmutableList<KeyCode> Add(KeyCode value)
  {
    return new ValidationErrorCollection([.._validationErrors.ToArray(), value]);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> AddRange(IEnumerable<KeyCode> items)
  {
    return new ValidationErrorCollection([.. _validationErrors.ToArray(), .. items]);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> Clear()
  {
    return new ValidationErrorCollection();
  }

  ///<inheritdoc/>
  public IEnumerator<KeyCode> GetEnumerator()
  {
    return _validationErrors.GetEnumerator();
  }

  ///<inheritdoc/>
  public int IndexOf(KeyCode item, int index, int count, IEqualityComparer<KeyCode>? equalityComparer)
  {
    return _validationErrors.IndexOf(item, index, count, equalityComparer);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> Insert(int index, KeyCode element)
  {
    var newlist = _validationErrors.ToList();
    newlist.Insert(index, element);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> InsertRange(int index, IEnumerable<KeyCode> items)
  {
    var newlist = _validationErrors.ToList();
    newlist.InsertRange(index, items);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public int LastIndexOf(KeyCode item, int index, int count, IEqualityComparer<KeyCode>? equalityComparer)
  {
    return _validationErrors.LastIndexOf(item, index, count, equalityComparer);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> Remove(KeyCode value, IEqualityComparer<KeyCode>? equalityComparer)
  {
    var newlist = _validationErrors.ToList();
    newlist.Remove(value);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> RemoveAll(Predicate<KeyCode> match)
  {
    var newlist = _validationErrors.ToList();
    newlist.RemoveAll(match);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> RemoveAt(int index)
  {
    var newlist = _validationErrors.ToList();
    newlist.RemoveAt(index);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> RemoveRange(IEnumerable<KeyCode> items, IEqualityComparer<KeyCode>? equalityComparer)
  {
    var newlist = _validationErrors.ToList();
    foreach (var item in items)
    {
      newlist.Remove(item);
    }
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> RemoveRange(int index, int count)
  {
    var newlist = _validationErrors.ToList();
    newlist.RemoveRange(index, count);
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> Replace(KeyCode oldValue, KeyCode newValue, IEqualityComparer<KeyCode>? equalityComparer)
  {
    var newlist = _validationErrors.ToList();
    var index = newlist.IndexOf(oldValue);
    newlist[index] = newValue;
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  public IImmutableList<KeyCode> SetItem(int index, KeyCode value)
  {
    var newlist = _validationErrors.ToList();
    newlist[index] = value;
    return new ValidationErrorCollection(newlist);
  }

  ///<inheritdoc/>
  IEnumerator IEnumerable.GetEnumerator()
  {
    return _validationErrors.GetEnumerator();
  }

  /// <inheritdoc/>
  public override string ToString()
  {
    return string.Join(", ", _validationErrors.Select(e => e.ToString()));
  }
}
