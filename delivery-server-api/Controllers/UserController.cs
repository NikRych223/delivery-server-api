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

        [Route("loginUser")]
        [HttpPost]
        public async Task<IActionResult> PostLoginUser([FromForm] string userName, [FromForm] string password)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(userName);

                    if (user == null) return BadRequest("User not foud");

                    var result = await _signInManager.PasswordSignInAsync(user, password, false, false);

                    if (result.Succeeded) return Ok("User logged");

                    return BadRequest("Username or Password not valid");
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("signOut")]
        [HttpPost]
        public async Task<IActionResult> PostSignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Ok("User sign out");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
