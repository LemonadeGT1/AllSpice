namespace AllSpice.Services;

public class IngredientsService
{
  private readonly IngredientsRepository _repo;

  public IngredientsService(IngredientsRepository repo)
  {
    _repo = repo;
  }

  internal Ingredient MakeIngredient(Ingredient ingredientData)
  {
    Ingredient ingredient = _repo.MakeIngredient(ingredientData);
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    List<Ingredient> ingredients = _repo.GetIngredientsByRecipeId(recipeId);
    if (ingredients == null)
    {
      throw new Exception($"This is an invalid recipe id: {recipeId}");
    }
    return ingredients;
  }

  internal string DeleteRecipe(int ingredientId)
  {
    Ingredient ingredient = _repo.GetIngredientById(ingredientId);
    if (ingredient == null)
    {
      throw new Exception("This ingredient does not exist in the database.");
    }
    _repo.RemoveIngredient(ingredientId);
    return ("The ingredient was successfully deleted.");
  }

}
