using System.Collections.Generic;
using System;
using RecipeBox;
using MySql.Data.MySqlClient;
using System.Globalization;

namespace RecipeBox.Models
{
  public class Tag
  {
    private int _id;
    private string _tagName;

    public Tag(string tagName, int Id = 0)
    {
      _id = Id;
      _tagName = tagName;
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"TRUNCATE TABLE tags;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    // public override bool Equals(System.Object otherTag)
    // {
    //   if (!(otherTag is Tag))
    //   {
    //     return false;
    //   }
    //   else
    //   {
    //     Tag newTag = (Tag) otherTag;
    //     bool idEquality = (this.GetId() == newTag.GetId());
    //     bool nameEquality = (this.GetName() == newTag.GetName());
    //     return (idEquality && nameEquality);
    //   }
    // }
    //
    // public override int GetHashCode()
    // {
    //   return this.GetId().GetHashCode();
    // }


  }
}
