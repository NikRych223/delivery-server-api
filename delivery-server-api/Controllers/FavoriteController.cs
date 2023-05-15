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
        private readonly UserManager<UserDbModel> _userManager;
        private readonly FoodDBContext _dbContext;

        public FavoriteController(UserManager<UserDbModel> userManager, FoodDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Route("item")]
        [HttpPost]
        public async Task<IActionResult> PostAddFavoriteByUserName([FromBody] FavoriteAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user == null) return NotFound("User not valid");

                    // TODO - rename result!!! - renemed foodIdAndUserName
                    var foodIdAndUserName = await _dbContext.Favorite.SingleOrDefaultAsync(x => x.FoodId == model.FoodId && x.UserId == user.Id);

                    if (foodIdAndUserName != null) return BadRequest("Favorites find equels foodId");

                    // var foodItem = await _dbContext.FoodItems.FindAsync(model.FoodId);
                    // if (foodItem == null) return NotFound("Food item not found");
                    // food.Favorites.Add(new FavoriteItem { UserId = user.Id });

                    await _dbContext.Favorite.AddAsync(new FavoriteDbModel { FoodId = model.FoodId, UserId = user.Id });
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

        [Route("item")]
        [HttpGet]
        public async Task<IActionResult> GetAllFavoriteByUserName([FromQuery] string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null) return NotFound("User not found");

                var favoriteItems = await _dbContext.FoodItems
                    .Where(fd => 
                        _dbContext.Favorite
                            .Where(x => x.UserId == user.Id)
                            .Select(x => x.FoodId)
                            .Contains(fd.FoodId)
                    ).Select(x => new FavoriteViewModel { FoodId = x.FoodId, Title = x.Title, Price = x.Price }).ToListAsync();

                return Ok(favoriteItems);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("item")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOneByUserName([FromBody] FavoriteAddModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null) return NotFound("User not found");

                var favoriteItem = await _dbContext.Favorite.SingleOrDefaultAsync(x => x.FoodId == model.FoodId && x.UserId == user.Id);

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
