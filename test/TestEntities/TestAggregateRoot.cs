using VectorCode.DddCommon.Test.TestEntities.Values;

namespace VectorCode.DddCommon.Test.TestEntities;

public class TestAggregateRoot : AggregateRoot<int, TestEntity.Dto, int>
{
  public string Value { get; }

  public ChildValue Child { get; }

  public TestAggregateRoot(int id, int version, string value, ChildValue child) : base(id, version)
  {
    Value = value;
    Child = child;
  }

  public void AddEvent(int value)
  {
    AddEvent(new TestEvent(value));
  }

  public override TestEntity.Dto ToDto()
  {
    return new TestEntity.Dto(Id, Value, Child.ToDto());
  }
}
