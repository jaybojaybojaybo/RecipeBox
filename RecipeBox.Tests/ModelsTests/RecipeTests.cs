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
  }


}
