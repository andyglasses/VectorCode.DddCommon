using CodedVector.DddCommon.Test.TestEntities.Values;

namespace CodedVector.DddCommon.Test.TestEntities;

public partial class TestEntity
{
  public record Dto(int Id, string Value, ChildValue.Dto Child) { }
}
