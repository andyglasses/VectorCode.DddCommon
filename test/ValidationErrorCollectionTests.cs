using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VectorCode.Common;

namespace VectorCode.DddCommon.Test;

[TestFixture]
public class ValidationErrorCollectionTests
{
  [Test]
  public void ValidationErrorCollection_Add_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1")]);
    // Act
    var result = collection.Add(new KeyCode("key2", "code2"));
    // Assert
    Assert.That(collection, Has.Exactly(1).Items);
    Assert.That(result, Has.Exactly(2).Items);
  }

  [Test]
  public void ValidationErrorCollection_AddRange_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1")]);
    // Act
    var result = collection.AddRange(new[] { new KeyCode("key2", "code2"), new KeyCode("key3", "code3") });
    // Assert
    Assert.That(collection, Has.Exactly(1).Items);
    Assert.That(result, Has.Exactly(3).Items);
  }

  [Test]
  public void ValidationErrorCollection_Clear_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1")]);
    // Act
    var result = collection.Clear();
    // Assert
    Assert.That(collection, Has.Exactly(1).Items);
    Assert.That(result, Is.Empty);
  }

  [Test]
  public void ValidationErrorCollection_GetEnumerator_ShouldReturnEnumerator()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1")]);
    // Act
    var result = collection.GetEnumerator();
    // Assert
    Assert.That(result, Is.Not.Null);
  }

  [Test]
  public void ValidationErrorCollection_IndexOf_ShouldReturnIndex()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.IndexOf(new KeyCode("key2", "code2"), 0, 2, null);
    // Assert
    Assert.That(result, Is.EqualTo(1));
  }

  [Test]
  public void ValidationErrorCollection_Insert_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.Insert(1, new KeyCode("key1-5", "code1-5"));
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(3).Items);
    Assert.That(result[1], Is.EqualTo(new KeyCode("key1-5", "code1-5")));
  }

  [Test]
  public void ValidationErrorCollection_InsertRange_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.InsertRange(1, new[] { new KeyCode("key1-5", "code1-5"), new KeyCode("key1-6", "code1-6") });
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(4).Items);
    Assert.That(result[1], Is.EqualTo(new KeyCode("key1-5", "code1-5")));
    Assert.That(result[2], Is.EqualTo(new KeyCode("key1-6", "code1-6")));
  }

  [Test]
  public void ValidationErrorCollection_LastIndexOf_ShouldReturnIndex()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.LastIndexOf(new KeyCode("key1", "code1"), 1, 2, null);
    // Assert
    Assert.That(result, Is.EqualTo(0));
  }

  [Test]
  public void ValidationErrorCollection_Remove_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.Remove(new KeyCode("key1", "code1"), null);
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(1).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key2", "code2")));
  }

  [Test]
  public void ValidationErrorCollection_RemoveAll_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.RemoveAll(x => x.Key == "key1");
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(1).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key2", "code2")));
  }

  [Test]
  public void ValidationErrorCollection_RemoveAt_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.RemoveAt(0);
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(1).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key2", "code2")));
  }

  [Test]
  public void ValidationErrorCollection_RemoveRange_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code"), new KeyCode("key3", "code3")]);
    // Act
    var result = collection.RemoveRange([ new("key1", "code1"), new("key3", "code3")], null);
    // Assert
    Assert.That(collection, Has.Exactly(3).Items);
    Assert.That(result, Has.Exactly(1).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key2", "code")));
  }

  [Test]
  public void ValidationErrorCollection_Count_ShouldReturnCount()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.Count;
    // Assert
    Assert.That(result, Is.EqualTo(2));
  }

  [Test]
  public void ValidationErrorCollection_RemoveRange_ByIndex_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2"), new KeyCode("key3", "code3")]);
    // Act
    var result = collection.RemoveRange(0, 2);
    // Assert
    Assert.That(collection, Has.Exactly(3).Items);
    Assert.That(result, Has.Exactly(1).Items);
  }

  [Test]
  public void ValidationErrorCollection_Replace_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.Replace(new KeyCode("key1", "code1"), new KeyCode("key1", "code1-5"), null);
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(2).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key1", "code1-5")));
  }

  [Test]
  public void ValidationErrorCollection_SetItem_ShouldCreateNew()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.SetItem(0, new KeyCode("key1", "code1-5"));
    // Assert
    Assert.That(collection, Has.Exactly(2).Items);
    Assert.That(result, Has.Exactly(2).Items);
    Assert.That(result[0], Is.EqualTo(new KeyCode("key1", "code1-5")));
  }

  [Test]
  public void ValidationErrorCollection_ToString_ShouldReturnString()
  {
    // Arrange
    var collection = new ValidationErrorCollection([new KeyCode("key1", "code1"), new KeyCode("key2", "code2")]);
    // Act
    var result = collection.ToString();
    // Assert
    Assert.That(result, Is.EqualTo("key1: code1, key2: code2"));
  }


}
