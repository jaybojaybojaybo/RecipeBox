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
  }
}
