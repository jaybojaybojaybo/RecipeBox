using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RecipeBox.Models;

namespace RecipeBox.Tests
{
  [TestClass]
  public class RecipeTest : IDisposable
  {
      public void RecipeTests()
      {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test;";
      }

      public void Dispose()
      {
        Recipe.DeleteAll();
        Ingredient.DeleteAll();
        Tag.DeleteAll();
        ingredients_recipes.DeleteAll();
        tags_recipes.DeleteAll();
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_Recipe()
      {
        // Arrange
        Recipe firstRecipe = new Recipe("Roasted Artichokes", "5", "cook");
        Recipe secondRecipe = new Recipe("Roasted Artichokes", "5", "cook");

        //Act
        firstRecipe.Save();
        secondRecipe.Save();

        // Assert
        Assert.AreEqual(true, firstRecipe.GetName().Equals(secondRecipe.GetName()));
      }

      [TestMethod]
      public void Find_FindsRecipeInDatabase_Recipe()
      {
        //Arrange
        Recipe testRecipe = new Recipe("Roasted Artichokes", "5", "cook");
        testRecipe.Save();

        //Act
        Recipe foundRecipe = Recipe.Find(testRecipe.GetId());

        //Assert
        Assert.AreEqual(testRecipe.GetId(), foundRecipe.GetId());
      }

      [TestMethod]
      public void EditName_UpdateRecipeInDatabase_String()
      {
        //Arrange
        string firstName = "Artichoke";
        Recipe testRecipe = new Recipe(firstName, "5", "cook");
        testRecipe.Save();
        string secondName = "Red Pepper";

        //Act
        testRecipe.EditName(secondName);
        string result = Recipe.Find(testRecipe.GetId()).GetName();

        //Assert
        Assert.AreEqual(secondName, result);
      }

      [TestMethod]
      public void EditRating_UpdateRecipeInDatabase_String()
      {
        //Arrange
        string firstRating = "5";
        Recipe testRecipe = new Recipe("Roasted Artichokes", firstRating, "cook");
        testRecipe.Save();
        string secondRating = "4";

        //Act
        testRecipe.EditRating(secondRating);
        string result = Recipe.Find(testRecipe.GetId()).GetRating();

        //Assert
        Assert.AreEqual(secondRating, result);
      }

      [TestMethod]
      public void EditInstruction_UpdateRecipeInDatabase_String()
      {
        //Arrange
        string firstInstruction = "cook";
        Recipe testRecipe = new Recipe("Roasted Artichokes", "5", firstInstruction);
        testRecipe.Save();
        string secondInstruction = "don't cook";

        //Act
        testRecipe.EditInstruction(secondInstruction);
        string result = Recipe.Find(testRecipe.GetId()).GetInstruction();

        //Assert
        Assert.AreEqual(secondInstruction, result);
      }

      [TestMethod]
      public void Delete_DeleteOneRecipeInDatabase_True()
      {
        //Arrange
        Recipe firstRecipe = new Recipe("Roasted Artichokes", "5", "cook");
        Recipe secondRecipe = new Recipe("Spaghetti", "3", "boil");
        List<Recipe> testList = new List<Recipe>{secondRecipe};
        firstRecipe.Save();
        secondRecipe.Save();

        //Act
        int firstId = firstRecipe.GetId();
        firstRecipe.DeleteRecipe(firstId);
        List<Recipe> compareList = Recipe.GetAll();
        Console.WriteLine(compareList.Count);
        //Assert
        Assert.AreEqual(testList.Count, compareList.Count);
      }

      [TestMethod]
      public void AddIngredient_AddsIngredientToRecipe_IngredientList()
      {
        //Arrange
        Ingredient testIngredient1 = new Ingredient("Olive Oil");
        testIngredient1.Save();
        Ingredient testIngredient2 = new Ingredient("Red Peppers");
        testIngredient2.Save();

        Recipe firstRecipe =  new Recipe("Roasted Artichokes", "5", "cook");
        firstRecipe.Save();

        List<Ingredient> testList = new List<Ingredient>{testIngredient1, testIngredient2};

        //Act
        firstRecipe.AddIngredient(testIngredient1);
        firstRecipe.AddIngredient(testIngredient2);
        List<Ingredient> result = firstRecipe.GetIngredients();

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
      }

      [TestMethod]
      public void GetIngredients_ReturnsAllIngredientRecipes_IngredientList()
      {
        //Arrange
        Ingredient testIngredient1 = new Ingredient("Artichoke");
        testIngredient1.Save();

        Ingredient testIngredient2 = new Ingredient("Red Peppers");
        testIngredient2.Save();

        Recipe testRecipe1 = new Recipe("Roasted Artichokes", "5", "cook");
        testRecipe1.Save();


        //Act
        testRecipe1.AddIngredient(testIngredient1);
        testRecipe1.AddIngredient(testIngredient2);
        List<Ingredient> result = testRecipe1.GetIngredients();
        Console.WriteLine(result[1].GetName());
        List<Ingredient> testList = new List<Ingredient> {testIngredient1, testIngredient2};

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
        // CollectionAssert.AreEqual(testList, result);
      }

      [TestMethod]
      public void AddTag_AddsTagToRecipe_TagList()
      {
        //Arrange
        Tag testTag1 = new Tag("Olive Oil");
        testTag1.Save();
        Tag testTag2 = new Tag("Red Peppers");
        testTag2.Save();

        Recipe firstRecipe =  new Recipe("Roasted Artichokes", "5", "cook");
        firstRecipe.Save();

        List<Tag> testList = new List<Tag>{testTag1, testTag2};

        //Act
        firstRecipe.AddTag(testTag1);
        firstRecipe.AddTag(testTag2);
        List<Tag> result = firstRecipe.GetTags();

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
      }

      [TestMethod]
      public void GetTags_ReturnsAllTagRecipes_TagList()
      {
        //Arrange
        Tag testTag1 = new Tag("Artichoke");
        testTag1.Save();

        Tag testTag2 = new Tag("Red Peppers");
        testTag2.Save();

        Recipe testRecipe1 = new Recipe("Roasted Artichokes", "5", "cook");
        testRecipe1.Save();


        //Act
        testRecipe1.AddTag(testTag1);
        testRecipe1.AddTag(testTag2);
        List<Tag> result = testRecipe1.GetTags();
        List<Tag> testList = new List<Tag> {testTag1, testTag2};

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
        // CollectionAssert.AreEqual(testList, result);
      }
  }


}
