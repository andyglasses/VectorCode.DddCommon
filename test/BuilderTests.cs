using FluentAssertions;

namespace CodedVector.DddCommon.Test;

[TestFixture]
public class BuilderTests
{
  [Test]
  public void CanCreate_WhenNoValidationErrors_CreatesEntity()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(1)
      .WithValue("Test")
      .WithChild().WithValue("Hello");

    // Act
    var result = builder.CanCreate();
    var errors = builder.ValidationErrors;
    var act = () => builder.Create();

    // Assert
    result.Should().BeTrue();
    errors.Should().BeEmpty();
    act.Should().NotThrow();

  }

  [Test]
  public void Create_WhenHasValidationErrors_DoesNotCreateEntity()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(0)
      .WithValue("Test")
      .WithChild().WithValue("Hello");
    // Act
    var result = builder.CanCreate();
    var errors = builder.ValidationErrors;
    var act = () => builder.Create();

    // Assert
    result.Should().BeFalse();
    errors.Count.Should().Be(1);
    errors[0].Key.Should().Be("Id");
    act.Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void Create_WhenChildHasValidationErrors_ThrowsException()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(1)
      .WithValue("Test")
      .WithChild().WithValue("Hi");

    // Act
    var result = builder.CanCreate();
    var errors = builder.ValidationErrors;
    var act = () => builder.Create();

    // Assert
    result.Should().BeFalse();
    errors.Count.Should().Be(1);
    errors[0].Key.Should().Be("SubValue.Value");
    act.Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void Create_WhenHasStringValidationationErrors_CreatesCorrectCode()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(1)
      .WithValue("String")
      .WithChild().WithValue("Hello");
    // Act
    var result = builder.CanCreate();
    var errors = builder.ValidationErrors;
    var act = () => builder.Create();

    // Assert
    result.Should().BeFalse();
    errors.Count.Should().Be(1);
    errors[0].Key.Should().Be("Value");
    errors[0].Code.Should().Be("ValueString:value");
    act.Should().Throw<InvalidOperationException>();
  }

  [Test]
  public void Create_WhenHasStringListValidationationErrors_CreatesCorrectCode()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(1)
      .WithValue("List")
      .WithChild().WithValue("Hello");
    // Act
    var result = builder.CanCreate();
    var errors = builder.ValidationErrors;
    var act = () => builder.Create();

    // Assert
    result.Should().BeFalse();
    errors.Count.Should().Be(1);
    errors[0].Key.Should().Be("Value");
    errors[0].Code.Should().Be("ValueList:value1;value2");
    act.Should().Throw<InvalidOperationException>();
  }
}
