using CodedVector.DddCommon.Test.TestEntities.Values;

namespace CodedVector.DddCommon.Test.TestEntities;

public partial class TestEntity
{

  public class Builder : Builder<TestEntity, Dto>
  {
    private int _id;
    private string? _value;
    private ChildValue.Builder? childBuilder;
    public Builder WithId(int id)
    {
      _id = id;
      return this;
    }

    public Builder WithValue(string value)
    {
      _value = value;
      return this;
    }

    public ChildValue.Builder WithChild()
    {
      childBuilder = new ChildValue.Builder();
      return childBuilder;
    }

    public override void WithValuesFromDto(Dto dto)
    {
      WithId(dto.Id)
       .WithValue(dto.Value)
       .WithChild().WithValuesFromDto(dto.Child);
    }

    protected override TestEntity Build()
    {
      return new TestEntity(_id, _value!, childBuilder!.Create());
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
      else if(_value == "List")
      {
        AddValidationError("Value", "ValueList", new List<string> { "value1", "value2" });
      }
      else if(_value == "String")
      {
        AddValidationError("Value", "ValueString", "value");
      }

      if (_id < 1)
      {
        AddValidationError("Id", "IdInvalid", _id);
      }

      ValidateChildBuilder(childBuilder, false, "SubValue");
    }
  }
}
