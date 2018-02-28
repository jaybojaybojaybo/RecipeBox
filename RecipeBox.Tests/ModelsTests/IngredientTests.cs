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
  }
}
