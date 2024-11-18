using VectorCode.DddCommon.Test.TestEntities.Values;

namespace VectorCode.DddCommon.Test.TestEntities;

public partial class TestEntity
{
  public record Dto(int Id, string Value, ChildValue.Dto Child) { }
}
