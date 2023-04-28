namespace AllSpice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController : ControllerBase
{
  private readonly IngredientsService _ingredientsService;

  private readonly Auth0Provider _auth;

  public IngredientsController(IngredientsService ingredientsService, Auth0Provider auth)
  {
    _ingredientsService = ingredientsService;
    _auth = auth;
  }

  [HttpPost]
  [Authorize]
  public ActionResult<Ingredient> MakeIngredient([FromBody] Ingredient ingredientData)
  {
    try
    {
      Ingredient ingredient = _ingredientsService.MakeIngredient(ingredientData);
      return Ok(ingredient);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{ingredientId}")]
  public ActionResult<string> DeleteIngredient(int ingredientId)
  {
    try
    {
      string message = _ingredientsService.DeleteRecipe(ingredientId);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}