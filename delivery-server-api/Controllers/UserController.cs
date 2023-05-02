using delivery_server_api.Contexts;
using delivery_server_api.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FoodDBContext _dbContext;
        private readonly UserManager<FoodUserDbModel> _userManager;
        private readonly SignInManager<FoodUserDbModel> _signInManager;

        public UserController(FoodDBContext dbContext, UserManager<FoodUserDbModel> userManager, SignInManager<FoodUserDbModel> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("createUser")]
        [HttpPost]
        public async Task<IActionResult> PostCreateUser([FromForm] FoodUserFormModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new FoodUserDbModel { UserName = model.UserName, Email = model.EmailAddress, PhoneNumber = model.PhoneNumber, Addres = model.Addres };
                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok("User created");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
