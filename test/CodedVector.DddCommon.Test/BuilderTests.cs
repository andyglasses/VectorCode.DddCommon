using FluentAssertions;
using System.Diagnostics.CodeAnalysis;
using static CodedVector.DddCommon.Test.BuilderTests.TestEntity;

namespace CodedVector.DddCommon.Test;

[TestFixture]
public class BuilderTests
{
  [Test]
  public void CanCreate_WhenNoValidationErrors_ReturnsTrue()
  {
    // Arrange
    var builder = new TestBuilder();
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
  public void Create_WhenNoValidationErrors_CreatesEntity()
  {
    // Arrange
    var builder = new TestBuilder();
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
    var builder = new TestBuilder();
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

  [ExcludeFromCodeCoverage]
  public class TestEntity : BaseEntity<int, TestDto>
  {
    public string Value { get; }
    public ChildValueEntity Child { get; }

    private TestEntity(int id, string value, ChildValueEntity child) : base(id)
    {
      Value = value;
      Child = child;
    }
    public override TestDto ToDto()
    {
      return new TestDto(Id, Value, Child.ToDto());
    }

    public record TestDto(int Id, string Value, ChildValueEntity.TestChildDto ChildDto) { }

    public class TestBuilder : Builder<TestEntity, TestDto>
    {
      private int _id;
      private string? _value;
      private ChildValueEntity.TestChildBuilder? childBuilder;
      public TestBuilder WithId(int id)
      {
        _id = id;
        return this;
      }

      public TestBuilder WithValue(string value)
      {
        _value = value;
        return this;
      }

      public ChildValueEntity.TestChildBuilder WithChild()
      {
        childBuilder = new ChildValueEntity.TestChildBuilder();
        return childBuilder;
      }

      public override void WithValuesFromDto(TestDto dto)
      {
         WithId(dto.Id)
          .WithValue(dto.Value)
          .WithChild().WithValuesFromDto(dto.ChildDto);
      }

      protected override TestEntity Build()
      {
        return new TestEntity(_id, _value!, childBuilder!.Create());
      }

      protected override void Validate()
      {
        if(string.IsNullOrWhiteSpace(_value))
        {
          AddValidationError("Value", "ValueRequired");
        }
        else if(_value.Length < 3)
        {
          AddValidationError("Value", "ValueTooShort", 3);
        }

        if (_id < 1)
        {
          AddValidationError("Id", "IdInvalid", _id);
        }

        ValidateChildBuilder(childBuilder, false, "SubValue");
      }
    }

  }

  [ExcludeFromCodeCoverage]
  public record ChildValueEntity(string Value)
  {
    public TestChildDto ToDto()
    {
      return new TestChildDto(Value);
    }
    public record TestChildDto(string Value) { }

    public class TestChildBuilder : Builder<ChildValueEntity, TestChildDto>
    {
      private string? _value;

      public TestChildBuilder WithValue(string value)
      {
        _value = value;
        return this;
      }

      public override void WithValuesFromDto(TestChildDto dto)
      {
        WithValue(dto.Value);
      }

      protected override ChildValueEntity Build()
      {
        return new ChildValueEntity(_value!);
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
      }
    }
  }

}
