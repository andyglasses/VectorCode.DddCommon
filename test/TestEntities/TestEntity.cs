using System.Diagnostics.CodeAnalysis;
using VectorCode.DddCommon.Test.TestEntities.Values;

namespace VectorCode.DddCommon.Test.TestEntities;

[ExcludeFromCodeCoverage]
public partial class TestEntity : Entity<int, TestEntity.Dto>
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
