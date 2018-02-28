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
    private string _name;
    private string _rating;
    private string _instruction;

    public Recipe(string recipeName, string rating, string instruction, int Id = 0)
    {
      _id = Id;
      _name = recipeName;
      _rating = rating;
      _instruction = instruction;
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

    public override bool Equals(System.Object otherRecipe)
    {
      if (!(otherRecipe is Recipe))
      {
        return false;
      }
      else
      {
        Recipe newRecipe = (Recipe) otherRecipe;
        bool idEquality = (this.GetId() == newRecipe.GetId());
        bool nameEquality = (this.GetName() == newRecipe.GetName());
        bool ratingEquality = (this.GetRating() == newRecipe.GetRating());
        bool instructionEquality = (this.GetInstruction() == newRecipe.GetInstruction());
        return (idEquality && nameEquality && ratingEquality && instructionEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetId().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }

    public void SetId(int newId)
    {
      _id = newId;
    }

    public string GetName()
    {
      return _name;
    }

    public void SetName(string newRecipeName)
    {
      _name = newRecipeName;
    }

    public string GetRating()
    {
      return _rating;
    }

    public void SetRating(string newRating)
    {
      _rating = newRating;
    }

    public string GetInstruction()
    {
      return _instruction;
    }

    public void SetInstruction(string newInstruction)
    {
      _instruction = newInstruction;
    }


    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO recipes (name) VALUES (@RecipeName);";
     cmd.Parameters.Add(new MySqlParameter("@RecipeName", this._name));

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Recipe Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM recipes WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int recipeId = 0;
     string recipeName = "";
     string recipeRating = "";
     string recipeInstruction = "";

     while (rdr.Read())
     {
      recipeId = rdr.GetInt32(0);
      recipeName = rdr.GetString(1);
      recipeRating = rdr.GetString(2);
      recipeInstruction = rdr.GetString(3);
     }

     Recipe foundRecipe= new Recipe(recipeName, recipeRating, recipeInstruction, recipeId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundRecipe;
    }

    public void EditName(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE recipes SET name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));

      cmd.Parameters.Add(new MySqlParameter("@newname", newName));

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void EditRating(string newRating)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE recipes SET rating = @newRating WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));

      cmd.Parameters.Add(new MySqlParameter("@newrating", newRating));

      cmd.ExecuteNonQuery();
      _rating = newRating;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void EditInstruction(string newInstruction)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE recipes SET instruction = @newInstruction WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _id));

      cmd.Parameters.Add(new MySqlParameter("@newinstruction", newInstruction));

      cmd.ExecuteNonQuery();
      _instruction = newInstruction;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Recipe> GetAll()
    {
      List<Recipe> allRecipes = new List<Recipe> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM recipes;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int recipeId = rdr.GetInt32(0);
        string recipeName = rdr.GetString(1);
        string recipeRating = rdr.GetString(2);
        string recipeInstruction = rdr.GetString(3);
        Recipe newRecipe = new Recipe(recipeName, recipeRating, recipeInstruction, recipeId);
        allRecipes.Add(newRecipe);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allRecipes;
    }

    public void DeleteRecipe(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM recipes WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
