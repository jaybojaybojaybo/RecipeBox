using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System;
using System.Collections.Generic;

namespace RecipeBox.Controllers
{
  public class IngredientsController : Controller
  {
    [HttpGet("/ingredients")]
    public ActionResult Index()
    {
      List<Ingredient> allIngredients = Ingredient.GetAll();
      return View(allIngredients);
    }

    [HttpGet("/ingredients/create")]
    public ActionResult CreateForm()
    {
      Dictionary<string, object> model = new Dictionary<string, object> {};
      List<Recipe> allRecipes = Recipe.GetAll();
      List<Ingredient> allIngredients = Ingredient.GetAll();
      List<Tag> allTags = Tag.GetAll();
      model.Add("allRecipes", allRecipes);
      model.Add("allIngredients", allIngredients);
      model.Add("allTags", allTags);
      return View(model);
    }

    [HttpPost("/ingredients/create")]
    public ActionResult Create()
    {
      Ingredient newIngredient = new Ingredient(Request.Form["name"]);
      newIngredient.Save();

      List<Ingredient> allIngredients = Ingredient.GetAll();
      return View("Index", allIngredients);
    }

    [HttpGet("/ingredients/{id}/delete")]
    public ActionResult DeleteIngredient(int id)
    {
      Ingredient findIngredient = Ingredient.Find(id);
     findIngredient.DeleteIngredient(findIngredient.GetId());
     List<Ingredient> allIngredients = Ingredient.GetAll();
     return RedirectToAction("Index");
    }
  }
}
