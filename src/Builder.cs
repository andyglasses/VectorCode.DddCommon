using VectorCode.Common;

namespace VectorCode.DddCommon;

/// <summary>
/// Entity builder class
/// </summary>
/// <typeparam name="TEntity">Entity Type</typeparam>
/// <typeparam name="TDto">Dto type</typeparam>
public abstract class Builder<TEntity, TDto>
{
  private readonly List<KeyCode> _valdiationErrors = new();

  /// <summary>
  /// Current Validation errors
  /// </summary>
  public ValidationErrorCollection ValidationErrors => new ValidationErrorCollection(_valdiationErrors);

  /// <summary>
  /// Create the entity, will throw an error if there are validation errors
  /// </summary>
  /// <returns>The built entity</returns>
  public TEntity Create()
  {
    if (!_markAsExisting && !CanCreate())
    {
      var ex = new InvalidOperationException("Cannot create entity with validation errors");
      ex.Data.Add("ValidationErrors", ValidationErrors);
      throw ex;
    }

    return Build();
  }

  /// <summary>
  /// Validates the builder and returns if it can create the entity
  /// </summary>
  /// <returns></returns>
  public bool CanCreate()
  {
    _valdiationErrors.Clear();
    Validate();
    return _valdiationErrors.Count == 0;
  }

  /// <summary>
  /// The method to override to validate the builder
  /// </summary>
  protected abstract void Validate();

  /// <summary>
  /// The method to override to build the entity
  /// </summary>
  /// <returns></returns>
  protected abstract TEntity Build();

  /// <summary>
  /// Override this method set values that are needed to indicate to the builder that this 
  /// is not a new but existing entity being created (e.g. from a data store)
  /// If not overwritten or called from the override method, the builder will assume this is an existing entity and ignore validation
  /// </summary>
  public virtual Builder<TEntity, TDto> MarkAsExisting()
  {
    _markAsExisting = true;
    return this;
  }

  private bool _markAsExisting;

  /// <summary>
  /// Add a validation error
  /// </summary>
  /// <param name="key">Validation property key</param>
  /// <param name="code">Validation error code</param>
  protected void AddValidationError(string key, string code)
  {
    _valdiationErrors.Add(new KeyCode(key, code));
  }

  /// <summary>
  /// Add a validation error with an integer description in the validation code
  /// </summary>
  /// <param name="key">Validation property key</param>
  /// <param name="code">Validation error code</param>
  /// <param name="intValue">Integer error code payload</param>
  protected void AddValidationError(string key, string code, int intValue)
  {
    _valdiationErrors.Add(KeyCode.Builder.KeyCodeWithNumberDetail(key, code, intValue));
  }

  /// <summary>
  /// Add a validation error with an integer description in the validation code
  /// </summary>
  /// <param name="key">Validation property key</param>
  /// <param name="code">Validation error code</param>
  /// <param name="stringValue">String error code payload</param>
  protected void AddValidationError(string key, string code, string stringValue)
  {
    _valdiationErrors.Add(KeyCode.Builder.KeyCodeWithStringDetail(key, code, stringValue));
  }

  /// <summary>
  /// Add a validation error with an integer description in the validation code
  /// </summary>
  /// <param name="key">Validation property key</param>
  /// <param name="code">Validation error code</param>
  /// <param name="stringValues">List of string error code payload values</param>
  protected void AddValidationError(string key, string code, List<string> stringValues)
  {
    _valdiationErrors.Add(KeyCode.Builder.KeyCodeWithStringListDetail(key, code, stringValues));
  }

  /// <summary>
  /// Add a validation KeyCode
  /// </summary>
  /// <param name="error">The error key code</param>
  protected void AddValidationError(KeyCode? error)
  {
    if (error != null)
    {
      _valdiationErrors.Add(error);
    }
  }

  /// <summary>
  /// Validate a child builder
  /// </summary>
  /// <typeparam name="TChildEntity">The child entity type</typeparam>
  /// <typeparam name="TChildDto">The child Dto type</typeparam>
  /// <param name="childBuilder">The child builder</param>
  /// <param name="excludePrefix">Should the property prefix be excluded</param>
  /// <param name="prefixOverride">Override the property prefix</param>
  protected void ValidateChildBuilder<TChildEntity, TChildDto>(Builder<TChildEntity, TChildDto>? childBuilder, bool excludePrefix = false, string? prefixOverride = null)
  {
    if (childBuilder == null) return;
    if (!childBuilder.CanCreate())
    {
      foreach (var error in childBuilder.ValidationErrors)
      {
        var prefix = excludePrefix ? string.Empty : $"{prefixOverride ?? typeof(TChildEntity).Name}.";
        AddValidationError($"{prefix}{error.Key}", error.Code);
      }
    }
  }

  /// <summary>
  /// Create an entity from a Dto, this will error if there are validation errors
  /// </summary>
  /// <param name="dto">The dto to parse</param>
  /// <returns>A valid entity</returns>
  public TEntity CreateFromDto(TDto dto)
  {
    WithValuesFromDto(dto);
    return Create();
  }

  /// <summary>
  /// Override Set the values of the builder from a Dto
  /// </summary>
  /// <param name="dto">The dto to parse</param>
  public abstract void WithValuesFromDto(TDto dto);
}
