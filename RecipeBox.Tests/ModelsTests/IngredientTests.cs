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
  }
}
