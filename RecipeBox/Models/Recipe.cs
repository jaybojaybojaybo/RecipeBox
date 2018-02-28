using System.Collections.Generic;
using System;
using RecipeBox;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace RecipeBox.Models
{
  public class Recipe
  {
    private int _id;
    private string _recipeName;

    public Recipe(string recipeName, int Id = 0)
    {
      _id = Id;
      _recipeName = recipeName;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE recipes;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
