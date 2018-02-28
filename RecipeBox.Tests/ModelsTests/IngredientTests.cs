using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using RecipeBox.Models;

namespace RecipeBox.Tests
{
  [TestClass]
  public class IngredientTest : IDisposable
  {
      public void IngredientTests()
      {
        DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test;";
      }

      public void Dispose()
      {
        Ingredient.DeleteAll();
        Recipe.DeleteAll();
        ingredients_recipes.DeleteAll();
      }

      [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_Ingredient()
      {
        // Arrange
        Ingredient firstIngredient = new Ingredient("Artichoke");
        Ingredient secondIngredient = new Ingredient("Artichoke");

        //Act
        firstIngredient.Save();
        secondIngredient.Save();


        // Assert
        Assert.AreEqual(true, firstIngredient.GetName().Equals(secondIngredient.GetName()));
      }

      [TestMethod]
      public void Find_FindsIngredientInDatabase_Ingredient()
      {
        //Arrange
        Ingredient testIngredient = new Ingredient("Artichoke");
        testIngredient.Save();

        //Act
        Ingredient foundIngredient = Ingredient.Find(testIngredient.GetId());

        //Assert
        Assert.AreEqual(testIngredient, foundIngredient);
      }

      [TestMethod]
      public void Edit_UpdateIngredientInDatabase_String()
      {
        //Arrange
        string firstName = "Artichoke";
        Ingredient testIngredient = new Ingredient(firstName);
        testIngredient.Save();
        string secondName = "Red Pepper";

        //Act
        testIngredient.Edit(secondName);
        string result = Ingredient.Find(testIngredient.GetId()).GetName();

        //Assert
        Assert.AreEqual(secondName, result);
      }

      [TestMethod]
      public void Delete_DeleteOneIngredientInDatabase_True()
      {
        //Arrange
        string firstName = "Artichokes";
        string secondName = "Red Peppers";
        Ingredient firstIngredient = new Ingredient(firstName);
        Ingredient secondIngredient = new Ingredient(secondName);
        List<Ingredient> testList = new List<Ingredient>{secondIngredient};
        firstIngredient.Save();
        secondIngredient.Save();

        //Act
        int firstId = firstIngredient.GetId();
        firstIngredient.DeleteIngredient(firstId);
        List<Ingredient> compareList = Ingredient.GetAll();
        Console.WriteLine(compareList.Count);
        //Assert
        Assert.AreEqual(testList.Count, compareList.Count);
      }

      [TestMethod]
      public void AddRecipe_AddsRecipeToIngredient_RecipeList()
      {
        //Arrange
        Ingredient testIngredient1 = new Ingredient("Olive Oil");
        testIngredient1.Save();

        Recipe firstRecipe =  new Recipe("Roasted Artichokes", "5", "cook");
        firstRecipe.Save();
        Recipe secondRecipe = new Recipe("Spaghetti", "3", "boil");
        secondRecipe.Save();
        List<Recipe> testList = new List<Recipe>{firstRecipe, secondRecipe};

        //Act
        testIngredient1.AddRecipe(firstRecipe);
        testIngredient1.AddRecipe(secondRecipe);
        List<Recipe> result = testIngredient1.GetRecipes();

        //Assert
        Assert.AreEqual(testList.Count, result.Count);
      }

      // [TestMethod]
      // public void GetRecipes_ReturnsAllIngredientRecipes_RecipeList()
      // {
      //   //Arrange
      //   Ingredient testIngredient1 = new Ingredient("Artichoke");
      //   testIngredient1.Save();
      //
      //   Recipe testRecipe1 = new Recipe("Roasted Artichokes", "5", "cook");
      //   testRecipe1.Save();
      //
      //   Recipe testRecipe2 = new Recipe("Spaghetti", "3", "boil");
      //   testRecipe2.Save();
      //
      //   //Act
      //   testIngredient1.AddRecipe(testRecipe1);
      //   List<Recipe> result = testIngredient1.GetRecipes();
      //   List<Recipe> testList = new List<Recipe> {testRecipe1};
      //
      //   //Assert
      //   Assert.AreEqual(testList.Count, result.Count);
      //   // CollectionAssert.AreEqual(testList, result);
      // }


  }
}
