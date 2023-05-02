namespace AllSpice.Repositories;

public class FavoritesRepository
{
  private readonly IDbConnection _db;

  public FavoritesRepository(IDbConnection db)
  {
    _db = db;
  }

  public Favorite AddFavorite(Favorite favoriteData)
  {
    string sql = @"
    INSERT INTO favorites(accountId, recipeId) 
    VALUES (@accountId, @recipeId);
    
    SELECT f.*, creator.*
    FROM favorites f
    JOIN accounts creator ON f.accountId = creator.id
    WHERE f.id = LAST_INSERT_ID()
    ;";
    Favorite favorite = _db.Query<Favorite, Account, Favorite>(sql, (f, creator) =>
    {
      f.Creator = creator;
      return f;
    }, favoriteData).FirstOrDefault();
    return favorite;
  }

  internal List<MyFavoriteRecipe> GetMyFavoriteRecipes(string userId)
  {
    string sql = @"
    SELECT r.*, a.*, f.*
    FROM recipes r 
    JOIN favorites f ON r.id = f.recipeId
    JOIN accounts a ON r.creatorId = a.id
    WHERE f.accountId = @userId
    ;";

    List<MyFavoriteRecipe> recipes = _db.Query<MyFavoriteRecipe, Account, Favorite, MyFavoriteRecipe>(sql, (r, a, f) =>
    {
      r.Creator = a;
      r.FavoriteId = f.Id;
      return r;
    }, new { userId }).ToList();
    return recipes;
  }

  public Favorite GetOne(int id)
  {
    string sql = "SELECT * FROM favorites WHERE id = @id;";
    Favorite favorite = _db.Query<Favorite>(sql, new { id }).FirstOrDefault();
    return favorite;
  }

  internal void DeleteFavorite(int id)
  {
    string sql = "DELETE FROM favorites WHERE id = @id;";
    _db.Execute(sql, new { id });
  }
}
