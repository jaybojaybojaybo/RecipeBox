using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System;
using System.Collections.Generic;


namespace RecipeBox.Controllers
{
  public class RecipesController : Controller
  {
    [HttpGet("/recipes")]
    public ActionResult Index()
    {
      List<Recipe> allRecipes = Recipe.GetAll();
      return View(allRecipes);
    }

    [HttpGet("/recipes/create")]
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

    [HttpPost("/recipes/create")]
    public ActionResult Create()
    {
      Recipe newRecipe = new Recipe(
      Request.Form["name"],
      Request.Form["rating"],
      Request.Form["instruction"]
      );
      newRecipe.Save();
      Console.WriteLine(newRecipe.GetRating());
      Ingredient newIngredient = new Ingredient(Request.Form["ingredientList"]);
      newRecipe.AddIngredient(newIngredient);
      Tag newTag = new Tag(Request.Form["tagList"]);
      newRecipe.AddTag(newTag);

      List<Recipe> allRecipes = Recipe.GetAll();
      return View("Index", allRecipes);
    }

    [HttpGet("/recipes/{id}/delete")]
    public ActionResult DeleteRecipe(int id)
    {
      Recipe findRecipe = Recipe.Find(id);
     findRecipe.DeleteRecipe(findRecipe.GetId());
     List<Recipe> allRecipes = Recipe.GetAll();
     return RedirectToAction("Index");
    }

    [HttpGet("/recipes/{id}/details")]
    public ActionResult Details(int id)
    {
       Dictionary<string, object> model = new Dictionary<string, object>();
      Recipe recipeInput = Recipe.Find(id);
      List<Ingredient> allIngredients = recipeInput.GetIngredients();
      List<Tag> allTags = recipeInput.GetTags();

      model.Add("tags", allTags);
      model.Add("recipes", recipeInput);
      model.Add("ingredients", allIngredients);

      return View(model);
    }

    [HttpGet("/recipes/wally")]
    public ActionResult Wally()
    {
      return View();
    }


  }
}
