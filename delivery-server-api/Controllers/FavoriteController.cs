using delivery_server_api.Contexts;
using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.Favorite;
using delivery_server_api.Models.FoodModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly UserManager<FoodUserDbModel> _userManager;
        private readonly FoodDBContext _dbContext;

        public FavoriteController(UserManager<FoodUserDbModel> userManager, FoodDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Route("addFavorite")]
        [HttpPost]
        public async Task<IActionResult> PostAddFavoriteByUserName([FromForm] FavoriteFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user == null) return NotFound("User not valid");

                    // TODO - rename result!!!
                    var result = await _dbContext.Favorite.SingleOrDefaultAsync(x => x.FoodId == model.FoodId && x.UserId == user.Id);

                    if (result != null) return BadRequest("Favorites find equels foodId");

                    var food = await _dbContext.FoodItems.FindAsync(model.FoodId);

                    if (food == null) return NotFound("Food item not found");

                    food.Favorites.Add(new FavoriteItem { UserId = user.Id });
                    await _dbContext.SaveChangesAsync();

                    return Ok("Favorite added");
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("getAll")]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteByUserName([FromForm] string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null) return NotFound("User not valid");

                var favoriteItems = _dbContext.Favorite.Where(x => x.UserId == user.Id).Select(x => x.FoodId).ToList();

                List<FoodDbModel> itemList = new();

                foreach (var item in favoriteItems)
                {
                    var newItem = await _dbContext.FoodItems.SingleOrDefaultAsync(x => x.FoodId == item);

                    if (newItem == null) continue;

                    itemList.Add(newItem);
                }

                if (user == null) return NotFound("Foods not found");

                return Ok(itemList);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("removeOne")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOneByUserName([FromForm] string userName, [FromForm] Guid foodId)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null) return NotFound("User not valid");

                var favoriteItem = await _dbContext.Favorite.SingleOrDefaultAsync(x => x.FoodId == foodId);

                if (favoriteItem == null) return NotFound("Favorite item not found");

                user.Favorites.Remove(favoriteItem);
                await _dbContext.SaveChangesAsync();

                return Ok("Favorite item removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
