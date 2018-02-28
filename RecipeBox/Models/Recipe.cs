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
    private string _rating;
    private string _instruction;

    public Recipe(string recipeName, string rating, string instruction, int Id = 0)
    {
      _id = Id;
      _recipeName = recipeName;
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

    // public override bool Equals(System.Object otherRecipe)
    // {
    //   if (!(otherRecipe is Recipe))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Recipe newRecipe = (Recipe) otherRecipe;
    //     bool idEquality = (this.GetId() == newRecipe.GetId());
    //     bool nameEquality = (this.GetName() == newRecipe.GetName());
    //     bool ratingEquality = (this.GetRating() == newRecipe.GetRating());
    //     bool instructionEquality = (this.GetInstruction() == newRecipe.GetInstruction());
    //     return (idEquality && nameEquality && ratingEquality && instructionEquality);
    //   }
    // }
    //
    // public override int GetHashCode()
    // {
    //   return this.GetId().GetHashCode();
    // }
  }
}
