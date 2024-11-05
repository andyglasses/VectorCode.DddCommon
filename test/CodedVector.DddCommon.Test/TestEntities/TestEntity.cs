using System.Diagnostics.CodeAnalysis;
using CodedVector.DddCommon.Test.TestEntities.Values;

namespace CodedVector.DddCommon.Test.TestEntities;

[ExcludeFromCodeCoverage]
public partial class TestEntity : BaseEntity<int, TestEntity.Dto>
{
  public string Value { get; }
  public ChildValue Child { get; }

  public TestEntity(int id, string value, ChildValue child) : base(id)
  {
    Value = value;
    Child = child;
  }
  public override Dto ToDto()
  {
    return new Dto(Id, Value, Child.ToDto());
  }


}
