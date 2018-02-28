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
    private string _name;

    public Ingredient(string ingredientName, int Id = 0)
    {
      _id = Id;
      _name = ingredientName;
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

    public override bool Equals(System.Object otherIngredient)
    {
      if (!(otherIngredient is Ingredient))
      {
        return false;
      }
      else
      {
        Ingredient newIngredient = (Ingredient) otherIngredient;
        bool idEquality = (this.GetId() == newIngredient.GetId());
        bool nameEquality = (this.GetName() == newIngredient.GetName());
        return (idEquality && nameEquality);
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

    public void SetName(string newIngredientName)
    {
      _name = newIngredientName;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO ingredients (name) VALUES (@IngredientName);";
     cmd.Parameters.Add(new MySqlParameter("@IngredientName", this._name));

     cmd.ExecuteNonQuery();
     _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Ingredient Find(int id)
    {
     MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
     cmd.CommandText = @"SELECT * FROM ingredients WHERE id = @searchId;";

     MySqlParameter searchId = new MySqlParameter();
     searchId.ParameterName = "@searchId";
     searchId.Value = id;
     cmd.Parameters.Add(searchId);

     var rdr = cmd.ExecuteReader() as MySqlDataReader;

     int ingredientId = 0;
     string ingredientName = "";

     while (rdr.Read())
     {
       ingredientId = rdr.GetInt32(0);
       ingredientName = rdr.GetString(1);
     }

     Ingredient foundIngredient= new Ingredient(ingredientName, ingredientId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

     return foundIngredient;
    }

    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE ingredients SET name = @newName WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@newname";
      name.Value = newName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _name = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static List<Ingredient> GetAll()
    {
      List<Ingredient> allIngredients = new List<Ingredient> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM ingredients;";
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int ingredientId = rdr.GetInt32(0);
        string ingredientName = rdr.GetString(1);
        Ingredient newIngredient = new Ingredient(ingredientName, ingredientId);
        allIngredients.Add(newIngredient);
      }
      conn.Close();
      if (conn != null)
      {
          conn.Dispose();
      }
      return allIngredients;
    }

    public void DeleteIngredient(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM ingredients WHERE id = @searchId;";

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

    public void AddRecipe(Recipe newRecipe)
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"INSERT INTO ingredients_recipes (recipe_id, ingredient_id) VALUES (@RecipeId, @IngredientId);";

        cmd.Parameters.Add(new MySqlParameter("@RecipeId", newRecipe.GetId()));
        cmd.Parameters.Add(new MySqlParameter("@IngredientId", _id));

        cmd.ExecuteNonQuery();
        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
    }

    public List<Recipe> GetRecipes()
    {
        MySqlConnection conn = DB.Connection();
        conn.Open();
        var cmd = conn.CreateCommand() as MySqlCommand;
        cmd.CommandText = @"SELECT recipes.* FROM ingredients
          JOIN ingredients_recipes ON (ingredients.id = ingredients_recipes.ingredient_id)
          JOIN recipes ON (ingredients_recipes.recipe_id = recipes.id)
          WHERE ingredients.id = @IngredientId;";

        MySqlParameter itemIdParameter = new MySqlParameter();
        itemIdParameter.ParameterName = "@IngredientId";
        itemIdParameter.Value = _id;
        cmd.Parameters.Add(itemIdParameter);

        MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;

        List<Recipe> ingredients = new List<Recipe> {};
        while(rdr.Read())
        {
            int recipeId = rdr.GetInt32(0);
            string recipeName = rdr.GetString(1);
            string recipeRating = rdr.GetString(2);
            string recipeInstruction = rdr.GetString(3);
            Recipe foundRecipe = new Recipe(recipeName, recipeRating, recipeInstruction, recipeId);
            ingredients.Add(foundRecipe);
        }
        rdr.Dispose();

        conn.Close();
        if (conn != null)
        {
            conn.Dispose();
        }
        return ingredients;
    }
  }
}
