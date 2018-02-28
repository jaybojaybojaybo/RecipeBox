using System.Collections.Generic;
using System;
using RecipeBox;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace RecipeBox.Models
{
  public class ingredients_recipes
  {
    private int _id;
    private int _ingredient_id;
    private int _recipe_id;

    public ingredients_recipes(int id, int ingredient, int recipe)
    {
      _id = id;
      _ingredient_id = ingredient;
      _recipe_id = recipe;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE ingredients_recipes;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
