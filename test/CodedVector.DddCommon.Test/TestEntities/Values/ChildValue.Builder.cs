namespace CodedVector.DddCommon.Test.TestEntities.Values;

public partial record ChildValue
{
  public class Builder : Builder<ChildValue, Dto>
  {
    private string? _value;

    public Builder WithValue(string value)
    {
      _value = value;
      return this;
    }

    public override void WithValuesFromDto(Dto dto)
    {
      WithValue(dto.Value);
    }

    protected override ChildValue Build()
    {
      return new ChildValue(_value!);
    }

    protected override void Validate()
    {
      if (string.IsNullOrWhiteSpace(_value))
      {
        AddValidationError("Value", "ValueRequired");
      }
      else if (_value.Length < 3)
      {
        AddValidationError("Value", "ValueTooShort", 3);
      }
    }
  }
}
