using FluentAssertions;
using System.Diagnostics.CodeAnalysis;

namespace CodedVector.DddCommon.Test;

[TestFixture]
public class BaseEntityTests
{
  [Test]
  public void Equals_WhenSameTypeAndId_ReturnsTrue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test");
    var entity2 = new TestEntity(1, "Test");

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    result.Should().BeTrue();
  }

  [Test]
  public void Equals_WhenSameIdButDifferentValue_ReturnsTrue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test");
    var entity2 = new TestEntity(1, "Bob");

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    result.Should().BeTrue();
  }

  [Test]
  public void Equals_WhenDifferentId_ReturnsFalse()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test");
    var entity2 = new TestEntity(2, "Test");

    // Act
    var result = entity1.Equals(entity2);

    // Assert
    result.Should().BeFalse();
  }

  [Test]
  public void Equals_WhenNull_ReturnsFalse()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test");

    // Act
    var result = entity1.Equals(null);

    // Assert
    result.Should().BeFalse();
  }

  [Test]
  public void GetHashCode_WhenSameId_ReturnsSameValue()
  {
    // Arrange
    var entity1 = new TestEntity(1, "Test");
    var entity2 = new TestEntity(1, "Test");

    // Act
    var result1 = entity1.GetHashCode();
    var result2 = entity2.GetHashCode();

    // Assert
    result1.Should().Be(result2);
  }

  [Test]
  public void ToDto_WhenCalled_ReturnsDto()
  {
    // Arrange
    var entity = new TestEntity(1, "Test");

    // Act
    var result = entity.ToDto();

    // Assert
    result.Id.Should().Be(1);
    result.Value.Should().Be("Test");
  }


  [ExcludeFromCodeCoverage]
  public class TestEntity : BaseEntity<int, TestEntity.TestDto>
  {
    public record TestDto(int Id, string Value);
    public string Value { get; }

    public TestEntity(int id, string value) : base(id)
    {
      Value = value;
    }
    public override TestEntity.TestDto ToDto()
    {
      return new TestEntity.TestDto(Id, Value);
    }
  }
}
