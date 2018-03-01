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
  }
}
