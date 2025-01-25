using VectorCode.DddCommon.Test.TestEntities;
using System.Collections;

namespace VectorCode.DddCommon.Test;

[TestFixture]
public class EntityTests
{
  [Test]
  public void Equals_WhenSameTypeAndId_ReturnsTrue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));
    var entity2 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    Assert.That(result, Is.True);
  }

  [Test]
  public void Equals_WhenSameIdButDifferentValue_ReturnsTrue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));
    var entity2 = new TestEntity(1, "Bob", new TestEntities.Values.ChildValue("child"));

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    Assert.That(result, Is.True);
  }

  [Test]
  public void Equals_WhenDifferentId_ReturnsFalse()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));
    var entity2 = new TestEntity(2, "Test", new TestEntities.Values.ChildValue("child"));

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    Assert.That(result, Is.False);
  }

  [Test]
  public void Equals_WhenNull_ReturnsFalse()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));

    // Act
    var result = entity1.Equals(null);

    // Assert
    Assert.That(result, Is.False);
  }

  [Test]
  public void GetHashCode_WhenSameId_ReturnsSameValue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));
    var entity2 = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));

    // Act
    var result1 = entity1.GetHashCode();
    var result2 = entity2.GetHashCode();

    // Assert
    Assert.That(result1, Is.EqualTo(result2));
  }

  [Test]
  public void ToDto_WhenCalled_ReturnsDto()
  {
    // Arrange
    var entity = new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child"));

    // Act
    var result = entity.ToDto();

    // Assert
    Assert.That(result.Id, Is.EqualTo(1));
    Assert.That(result.Value, Is.EqualTo("Test"));
  }

  [Test]
  [TestCaseSource(nameof(EqualsOperator_TestCases))]
  public void EqualsOperator(TestEntity entity1, TestEntity entity2, bool equalsExpected)
  {
    // Arrange

    // Act
    var resultEq = entity1 == entity2;
    var resultNeq = entity1 != entity2;

    // Assert
    Assert.That(resultEq, Is.EqualTo(equalsExpected));
    Assert.That(resultNeq, Is.EqualTo(!equalsExpected));
  }

  private static IEnumerable EqualsOperator_TestCases
  {
    get
    {
      yield return new TestCaseData(
        new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child")), 
        new TestEntity(1, "Test2", new TestEntities.Values.ChildValue("child2")),
        true).SetName("Same Id");
      yield return new TestCaseData(
        new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child")),
        new TestEntity(2, "Test", new TestEntities.Values.ChildValue("child")),
        false).SetName("Different Id");
      yield return new TestCaseData(
        new TestEntity(1, "Test", new TestEntities.Values.ChildValue("child")),
        null,
        false).SetName("One null");
      yield return new TestCaseData(
        null,
        null,
        true).SetName("Both null");
      yield return new TestCaseData(
        new TestEntity(0, "Test", new TestEntities.Values.ChildValue("child")),
        new TestEntity(0, "Test", new TestEntities.Values.ChildValue("child")),
        false).SetName("Both default Ids");
    }
  }
}
