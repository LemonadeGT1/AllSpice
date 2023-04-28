namespace AllSpice.Repositories;

public class RecipesRepository
{
  private readonly IDbConnection _db;

  public RecipesRepository(IDbConnection db)
  {
    _db = db;
  }

  public Recipe MakeRecipe(Recipe recipeData)
  {
    string sql = @"
    INSERT INTO recipes(title, instructions, img, category, creatorId)
    VALUES(@Title, @Instructions, @Img, @Category, @CreatorId);

    SELECT r.*, creator.*
    FROM recipes r
    JOIN accounts creator ON r.creatorId = creator.id
    WHERE r.id = LAST_INSERT_ID()
    ;";

    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (r, creator) =>
    {
      r.Creator = creator;
      return r;
    }, recipeData).FirstOrDefault();
    return recipe;
  }

  public Recipe UpdateRecipe(Recipe recipeData)
  {
    string sql = @"
    UPDATE recipes
    SET
    title = @Title,
    instructions = @Instructions,
    img = @Img,
    category = @Category
    WHERE id = @Id
    ;";

    _db.Execute(sql, recipeData);
    return recipeData;
  }

  internal void DeleteRecipe(int id)
  {
    string sql = "DELETE FROM recipes WHERE id = @id;";
    _db.Execute(sql, new { id });
  }

  internal Recipe GetRecipeById(int recipeId)
  {
    string sql = @"
    SELECT r.*, creator.*
    FROM recipes r
    JOIN accounts creator ON r.creatorId = creator.id
    WHERE r.id = @recipeId
    ;";
    Recipe recipe = _db.Query<Recipe, Account, Recipe>(sql, (r, creator) =>
    {
      r.Creator = creator;
      return r;
    }, new { recipeId }).FirstOrDefault();
    return recipe;
  }

  internal List<Recipe> GetAllRecipes()
  {
    string sql = @"
    SELECT r.*, creator.*
    FROM recipes r, accounts creator
    WHERE r.creatorId = creator.id
    ;";
    List<Recipe> recipes = _db.Query<Recipe, Account, Recipe>(sql, (r, creator) =>
    {
      r.Creator = creator;
      return r;
    }).ToList();
    return recipes;
  }


}