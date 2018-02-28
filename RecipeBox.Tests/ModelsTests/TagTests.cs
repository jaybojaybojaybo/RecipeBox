using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RecipeBox.Models;

namespace RecipeBox.Tests
{
  [TestClass]
  public class TagTest : IDisposable
  {
      public void TagTests()
      {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test;";
      }

      public void Dispose()
      {
        Tag.DeleteAll();
        Recipe.DeleteAll();
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_Tag()
      {
        // Arrange
        Tag firstTag = new Tag("Artichoke");
        Tag secondTag = new Tag("Artichoke");

        //Act
        firstTag.Save();
        secondTag.Save();


        // Assert
        Assert.AreEqual(true, firstTag.GetName().Equals(secondTag.GetName()));
      }

      [TestMethod]
      public void Find_FindsTagInDatabase_Tag()
      {
        //Arrange
        Tag testTag = new Tag("Artichoke");
        testTag.Save();

        //Act
        Tag foundTag = Tag.Find(testTag.GetId());

        //Assert
        Assert.AreEqual(testTag, foundTag);
      }

      [TestMethod]
      public void Edit_UpdateTagInDatabase_String()
      {
        //Arrange
        string firstName = "Artichoke";
        Tag testTag = new Tag(firstName);
        testTag.Save();
        string secondName = "Red Pepper";

        //Act
        testTag.Edit(secondName);
        string result = Tag.Find(testTag.GetId()).GetName();

        //Assert
        Assert.AreEqual(secondName, result);
      }

      [TestMethod]
      public void Delete_DeleteOneTagInDatabase_True()
      {
        //Arrange
        string firstName = "Artichokes";
        string secondName = "Red Peppers";
        Tag firstTag = new Tag(firstName);
        Tag secondTag = new Tag(secondName);
        List<Tag> testList = new List<Tag>{secondTag};
        firstTag.Save();
        secondTag.Save();

        //Act
        int firstId = firstTag.GetId();
        firstTag.DeleteTag(firstId);
        List<Tag> compareList = Tag.GetAll();
        Console.WriteLine(compareList.Count);
        //Assert
        Assert.AreEqual(testList.Count, compareList.Count);
      }
  }
}
