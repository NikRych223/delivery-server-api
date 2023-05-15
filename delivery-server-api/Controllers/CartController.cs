using delivery_server_api.Contexts;
using delivery_server_api.Models.ApplicationUser;
using delivery_server_api.Models.Cart;
using delivery_server_api.Models.FoodModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly UserManager<UserDbModel> _userManager;
        private readonly FoodDBContext _dbContext;

        public CartController(UserManager<UserDbModel> userManager, FoodDBContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        [Route("item")]
        [HttpPost]
        public async Task<IActionResult> PostAddCartByUsername([FromBody] CartAddModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null) return NotFound("User not found");

                // 1) Additing similar
                // 2) Not additing similar, but add to cart db model counter (+)

                var sameFoodIdAndUsername = await _dbContext.Cart.SingleOrDefaultAsync(x => x.FoodId == model.FoodId && x.UserId == user.Id);

                if (sameFoodIdAndUsername != null)
                {
                    sameFoodIdAndUsername.CountItem++;
                    await _dbContext.SaveChangesAsync();

                    return Ok("Cart item plus");
                }

                await _dbContext.Cart.AddAsync(new CartDbModel { UserId = user.Id, FoodId = model.FoodId, CountItem = 1 });
                await _dbContext.SaveChangesAsync();

                return Ok("Cart item added");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("item")]
        [HttpGet]
        public async Task<IActionResult> GetAllCartByUsername([FromQuery] string userName)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(userName);

                if (user == null) return NotFound("User not found");

                var foodItemsByCartUser = await _dbContext.FoodItems
                    .Where(fd =>
                        _dbContext.Cart
                            .Where(x => x.UserId == user.Id)
                            .Select(x => x.FoodId)
                            .Contains(fd.FoodId)
                    ).SelectMany(fi =>
                        _dbContext.Cart.Where(x => x.FoodId == fi.FoodId && x.UserId == user.Id)
                            .Select(x => x.CountItem), (fi, count) => new
                            {
                                FoodItem = new FoodViewModel(fi.FoodId, fi.Title, fi.Price),
                                countItem = count
                            }
                    ).ToListAsync();

                if (foodItemsByCartUser == null) return NoContent();

                return Ok(foodItemsByCartUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("item")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItemOrMinusByUsername([FromBody] CartAddModel model)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null) return NotFound("User not found");

                var cartItem = await _dbContext.Cart.SingleOrDefaultAsync(x => x.FoodId == model.FoodId && x.UserId == user.Id);

                if (cartItem == null) return NotFound("Cart item not found");

                if (cartItem.CountItem != 1)
                {
                    cartItem.CountItem--;
                    await _dbContext.SaveChangesAsync();

                    return Ok("Cart item minus");
                }

                _dbContext.Cart.Remove(cartItem);
                await _dbContext.SaveChangesAsync();

                return Ok("Cart item removed");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
