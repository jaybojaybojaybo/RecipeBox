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
      return _ingredientName;
    }

    public void SetName(string newIngredientName)
    {
      _ingredientName = newIngredientName;
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
     conn.Open();

     var cmd = conn.CreateCommand() as MySqlCommand;
    cmd.CommandText = @"INSERT INTO ingredients (name) VALUES (@IngredientName);";
     cmd.Parameters.Add(new MySqlParameter("@IngredientName", this._ingredientName));

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





  }
}
