namespace AllSpice.Services;

public class RecipesService
{
  private readonly RecipesRepository _repo;

  public RecipesService(RecipesRepository repo)
  {
    _repo = repo;
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    Recipe recipe = _repo.GetRecipeById(recipeId);
    if (recipe == null)
    {
      throw new Exception($"This id is invalid: {recipeId}");
    }
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    List<Recipe> recipes = _repo.GetAllRecipes();
    if (recipes == null)
    {
      throw new Exception($"There are no recipes yet.");
    }
    return recipes;
  }

  internal Recipe MakeRecipe(Recipe recipeData)
  {
    Recipe recipe = _repo.MakeRecipe(recipeData);
    return recipe;
  }

  internal Recipe UpdateRecipe(Recipe recipeData)
  {
    Recipe originalRecipe = GetRecipeById(recipeData.Id);
    originalRecipe.Title = recipeData.Title == null ? originalRecipe.Title : recipeData.Title;
    originalRecipe.Instructions = recipeData.Instructions == null ? originalRecipe.Instructions : recipeData.Instructions;
    originalRecipe.Img = recipeData.Img == null ? originalRecipe.Img : recipeData.Img;
    originalRecipe.Category = recipeData.Category == null ? originalRecipe.Category : recipeData.Category;
    return _repo.UpdateRecipe(originalRecipe);
  }

  internal string DeleteRecipe(int recipeId, string userId)
  {
    Recipe recipe = GetRecipeById(recipeId);
    if (recipe.CreatorId != userId)
    {
      throw new Exception("You do not have permission to delete this recipe!");
    }
    else if (recipe == null)
    {
      throw new Exception("That recipe does not exist.");
    }
    _repo.DeleteRecipe(recipeId);
    return ("That recipe has successfully been deleted.");
  }
}
