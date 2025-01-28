namespace VectorCode.DddCommon.Test;

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
    Assert.That(result, Is.True);
    Assert.That(errors, Is.Empty);
    Assert.That(act, Throws.Nothing);

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
    Assert.That(result, Is.False);
    Assert.That(errors, Has.Exactly(1).Items);
    Assert.That(errors[0].Key, Is.EqualTo("Id"));
    Assert.That(act, Throws.InvalidOperationException);
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
    Assert.That(result, Is.False);
    Assert.That(errors, Has.Exactly(1).Items);
    Assert.That(errors[0].Key, Is.EqualTo("SubValue.Value"));
    Assert.That(act, Throws.InvalidOperationException);
  }

  [Test]
  public void Create_WhenHasStringValidationErrors_CreatesCorrectCode()
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
    Assert.That(result, Is.False);
    Assert.That(errors, Has.Exactly(1).Items);
    Assert.That(errors[0].Key, Is.EqualTo("Value"));
    Assert.That(errors[0].Code, Is.EqualTo("ValueString:value"));
    Assert.That(act, Throws.InvalidOperationException);
  }

  [Test]
  public void Create_WhenHasStringListValidationErrors_CreatesCorrectCode()
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
    Assert.That(result, Is.False);
    Assert.That(errors, Has.Exactly(1).Items);
    Assert.That(errors[0].Key, Is.EqualTo("Value"));
    Assert.That(errors[0].Code, Is.EqualTo("ValueList:value1;value2"));
    Assert.That(act, Throws.InvalidOperationException);
  }

  [Test]
  public void Create_WhenExisting_IgnoresErrors()
  {
    // Arrange
    var builder = new TestEntities.TestEntity.Builder();
    builder.WithId(1)
       .WithValue("")
       .WithChild().WithValue("Hello");

    // Act
    builder.CreatedFromExisting();
    var act = () => builder.Create();

    // Assert
    Assert.That(act, Throws.Nothing);
  }
}
