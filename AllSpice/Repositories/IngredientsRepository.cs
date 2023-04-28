namespace AllSpice.Repositories;

public class IngredientsRepository
{
  private readonly IDbConnection _db;

  public IngredientsRepository(IDbConnection db)
  {
    _db = db;
  }

  public Ingredient MakeIngredient(Ingredient ingredientData)
  {
    string sql = @"
    INSERT INTO ingredients (name, quantity, recipeId)
    VALUES(@Name, @Quantity, @RecipeId);

    SELECT i.*
    FROM ingredients i
    WHERE id = LAST_INSERT_ID()
    ;";

    Ingredient ingredient = _db.Query<Ingredient>(sql, ingredientData).FirstOrDefault();
    return ingredient;
  }

  internal List<Ingredient> GetIngredientsByRecipeId(int recipeId)
  {
    string sql = "SELECT * FROM ingredients WHERE ingredients.recipeId = @recipeId;";

    List<Ingredient> ingredients = _db.Query<Ingredient>(sql, new { recipeId }).ToList();
    return ingredients;
  }

  internal Ingredient GetIngredientById(int ingredientId)
  {
    string sql = "SELECT * from ingredients where ingredients.id = @ingredientId";
    Ingredient ingredient = _db.Query<Ingredient>(sql, new { ingredientId }).FirstOrDefault();
    return ingredient;
  }

  internal void RemoveIngredient(int id)
  {
    string sql = "DELETE FROM ingredients WHERE id = @id;";
    _db.Execute(sql, new { id });
  }
}
