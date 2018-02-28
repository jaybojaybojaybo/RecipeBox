##Specifications

###User Stories
* As a user, I want to add a recipe with ingredients and instructions, so I remember how to prepare my favorite dishes.
* As a user, I want to tag my recipes with different categories, so recipes are easier to find. A recipe can have many tags and a tag can have many recipes.
* As a user, I want to be able to update and delete tags, so I can have flexibility with how I categorize recipes.
* As a user, I want to edit my recipes, so I can make improvements or corrections to my recipes.
* As a user, I want to be able to delete recipes I don't like or use, so I don't have to see them as choices.
* As a user, I want to rate my recipes, so I know which ones are the best.
* As a user, I want to list my recipes by highest rated so I can see which ones I like the best.
* As a user, I want to see all recipes that use a certain ingredient, so I can more easily find recipes for the ingredients I have.

###Examples:

User can create an ingredient:
  example input: "artichoke"
  example output: "artichoke"

User can find an ingredient by searching:
  example search input: "artichoke"
  example search output: "artichoke"

User can update an ingredient:
  existing: "artichoke"
  example input: "artichokes"
  example output: "artichokes"

User can delete an ingredient:
  example input: "artichoke"
  example output:

User can create a recipe:
  example input:
    name:"Roasted Artichokes"
    ingredient: "artichoke"
    ingredient: "parmesan"
    ingredient: "olive oil"
    instructions: "10 min on high ..."
  example output:
    "Roasted Artichokes"
    "artichoke"
    "parmesan"
    "olive oil"
    "10 min on high ..."

User can find a recipe using search:
  example input:"Artichokes"
  example output:"Roasted Artichokes"

User can update a recipe:
  existing:
    name:"Roasted Artichokes"
    ingredient: "artichoke"
    ingredient: "parmesan"
    ingredient: "olive oil"
    instructions: "10 min on high ..."
  example input: "red pepper"
  example output:
    "Roasted Artichokes"
    "artichoke"
    "parmesan"
    "olive oil"
    "red pepper"
    "10 min on high ..."

User can delete a recipe:
  example input:
    name:"Roasted Artichokes"
    ingredient: "artichoke"
    ingredient: "parmesan"
    ingredient: "olive oil"
    instructions: "10 min on high ..."
  example output:""

User can create tags for recipes:
  example input: (for "Roasted Artichokes") "Italian"
  example output: "Italian"

User can find tags for recipes using search:
  example input: "Italian"
  example output: "Italian"

User can update tags for recipes:
  existing: (for "Roasted Artichokes") "Italian"
  example input: "Italian fusion"
  example output: "Italian fusion"

User can delete tags for recipes:
  example input: "Italian"
  example output: ""

User can rate recipe:
  example input: (for "Roasted Artichokes") - 5 stars
  example output: 5 stars

User can search recipes by rating:
  example input: 5 stars
  example output: "Roasted Artichokes"

User can update rating for recipe:
  existing: 5 stars
  example input: 4 stars
  example output: 4 stars





![](https://i.imgur.com/ugcnc2M.png)
