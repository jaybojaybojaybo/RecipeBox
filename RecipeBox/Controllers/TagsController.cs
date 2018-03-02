using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System;
using System.Collections.Generic;

namespace RecipeBox.Controllers
{
  public class TagsController : Controller
  {
    [HttpGet("/tags")]
    public ActionResult Index()
    {
      List<Tag> allTags = Tag.GetAll();
      return View(allTags);
    }

    [HttpGet("/tags/create")]
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

    [HttpPost("/tags/create")]
    public ActionResult Create()
    {
      Tag newTag = new Tag(Request.Form["name"]);
      newTag.Save();

      List<Tag> allTags = Tag.GetAll();
      return View("Index", allTags);
    }

    [HttpGet("/tags/{id}/delete")]
    public ActionResult DeleteTag(int id)
    {
      Tag findTag = Tag.Find(id);
     findTag.DeleteTag(findTag.GetId());
     List<Tag> allTags = Tag.GetAll();
     return RedirectToAction("Index");
    }
  }
}
