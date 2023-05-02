namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritesController : ControllerBase
{
  private readonly FavoritesService _favoritesService;
  private readonly Auth0Provider _auth;

  public FavoritesController(Auth0Provider auth, FavoritesService favoritesService)
  {
    _auth = auth;
    _favoritesService = favoritesService;
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Favorite>> AddFavorite([FromBody] Favorite favoriteData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      favoriteData.AccountId = userInfo.Id;
      Favorite favorite = _favoritesService.AddFavorite(favoriteData);
      return Ok(favorite);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{favoriteId}")]
  [Authorize]
  public async Task<ActionResult<string>> DeleteFavorite(int favoriteId)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _favoritesService.DeleteFavorite(favoriteId, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
