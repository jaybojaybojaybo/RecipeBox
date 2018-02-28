using System.Collections.Generic;
using System;
using RecipeBox;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace RecipeBox.Models
{
  public class Ingredient
  {
    private int _id;
    private string _ingredientName;

    public Ingredient(string ingredientName, int Id = 0)
    {
      _id = Id;
      _ingredientName = ingredientName;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE ingredients;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
