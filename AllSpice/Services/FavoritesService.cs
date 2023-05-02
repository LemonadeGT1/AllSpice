namespace AllSpice.Services;

public class FavoritesService
{
  private readonly FavoritesRepository _repo;

  public FavoritesService(FavoritesRepository repo)
  {
    _repo = repo;
  }

  internal Favorite AddFavorite(Favorite favoriteData)
  {
    Favorite favorite = _repo.AddFavorite(favoriteData);
    return favorite;
  }

  internal List<MyFavoriteRecipe> GetMyFavoriteRecipes(string userId)
  {
    List<MyFavoriteRecipe> recipes = _repo.GetMyFavoriteRecipes(userId);
    return recipes;
  }

  internal Favorite GetOne(int id)
  {
    Favorite favorite = _repo.GetOne(id);
    if (favorite == null)
    {
      throw new Exception("No such favorite found.");
    }
    return favorite;
  }

  internal string DeleteFavorite(int id, string userId)
  {
    Favorite favorite = GetOne(id);
    if (favorite.AccountId != userId)
    {
      throw new Exception("You are not authorized to perform this action.");
    }
    _repo.DeleteFavorite(id);
    return "That Recipe is no longer a Favorite.";
  }
}
