using VectorCode.DddCommon.Test.TestEntities;

namespace VectorCode.DddCommon.Test;

[TestFixture]
public class AggregateRootTests
{
  [Test]
  public void AggregateRoot_AddEvent_ShouldAddEvent()
  {
    // Arrange
    var aggregateRoot = new TestAggregateRoot(1, 1, "value1", new TestEntities.Values.ChildValue("child1"));

    // Act
    aggregateRoot.AddEvent(5);

    // Assert
    Assert.That(aggregateRoot.GetEvents().Count, Is.EqualTo(1));
    Assert.That(aggregateRoot.GetEvents()[0], Is.InstanceOf<TestEvent>());
    Assert.That(((TestEvent)aggregateRoot.GetEvents()[0]).Value, Is.EqualTo(5));
  }

  [Test]
  public void AggregateRoot_ClearEvents_ShouldClearEvents()
  {
    // Arrange
    var aggregateRoot = new TestAggregateRoot(1, 1, "value1", new TestEntities.Values.ChildValue("child1"));
    aggregateRoot.AddEvent(5);
    // Act
    aggregateRoot.ClearEvents();
    // Assert
    Assert.That(aggregateRoot.GetEvents().Count, Is.EqualTo(0));
  }
}
