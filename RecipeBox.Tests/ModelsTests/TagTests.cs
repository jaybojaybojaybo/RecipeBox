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
        tags_recipes.DeleteAll();
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

      [TestMethod]
      public void AddRecipe_AddsRecipeToTag_RecipeList()
      {
        //Arrange
        Tag testTag1 = new Tag("Olive Oil");
        testTag1.Save();

        Recipe firstRecipe =  new Recipe("Roasted Artichokes", "5", "cook");
        firstRecipe.Save();
        Recipe secondRecipe = new Recipe("Spaghetti", "3", "boil");
        secondRecipe.Save();
        List<Recipe> testList = new List<Recipe>{firstRecipe, secondRecipe};

        //Act
        testTag1.AddRecipe(firstRecipe);
        testTag1.AddRecipe(secondRecipe);
        List<Recipe> result = testTag1.GetRecipes();

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
      }

      [TestMethod]
      public void GetRecipes_ReturnsAllTagRecipes_RecipeList()
      {
        //Arrange
        Tag testTag1 = new Tag("Artichoke");
        testTag1.Save();

        Recipe testRecipe1 = new Recipe("Roasted Artichokes", "5", "cook");
        testRecipe1.Save();

        Recipe testRecipe2 = new Recipe("Spaghetti", "3", "boil");
        testRecipe2.Save();

        //Act
        testTag1.AddRecipe(testRecipe1);
        List<Recipe> result = testTag1.GetRecipes();
        List<Recipe> testList = new List<Recipe> {testRecipe1};

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
        // CollectionAssert.AreEqual(testList, result);
      }
  }
}
