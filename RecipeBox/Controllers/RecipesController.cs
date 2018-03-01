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
      return View();
    }

    [HttpPost("/recipes/create")]
    public ActionResult Create()
    {
      return View();
    }
  }
}
