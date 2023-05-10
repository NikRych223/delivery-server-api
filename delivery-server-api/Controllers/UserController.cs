using delivery_server_api.Contexts;
using delivery_server_api.Models.ApplicationUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly FoodDBContext _dbContext;
        private readonly UserManager<UserDbModel> _userManager;
        private readonly SignInManager<UserDbModel> _signInManager;

        public UserController(FoodDBContext dbContext, UserManager<UserDbModel> userManager, SignInManager<UserDbModel> signInManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> PostCreateUser([FromBody] UserSignUpModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = new UserDbModel
                    {
                        UserName = model.UserName,
                        Email = model.EmailAddress,
                        PhoneNumber = model.PhoneNumber,
                        Addres = model.Addres
                    };

                    var result = await _userManager.CreateAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return Ok("User sign up");
                    }
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("signin")]
        [HttpPost]
        public async Task<IActionResult> PostLoginUser([FromBody] UserLoginModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(model.UserName);

                    if (user == null) return BadRequest("User not foud");

                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

                    if (result.Succeeded) return Ok("User sign in");

                    return BadRequest("Username or Password not valid");
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("signout")]
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

        [Route("External")]
        [HttpGet]
        // TODO - change to POST, add provider, returnUrl
        public IActionResult PostExternalSignIn(string? provider = "Google", string? returnUrl = $@"https://localhost:7050/api/User/ExternalCallback")
        {
            try
            {
                var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, returnUrl);

                return Challenge(properties, provider);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("ExternalCallback")]
        [HttpGet]
        public async Task<IActionResult> GetExternalSignInCallback()
        {
            try
            {
                var externalInfo = await _signInManager.GetExternalLoginInfoAsync();

                if (externalInfo == null) return BadRequest();

                var externalSignIn = await _signInManager.ExternalLoginSignInAsync(externalInfo.LoginProvider, externalInfo.ProviderKey, false, true);

                if (externalSignIn.Succeeded)
                {
                    return Ok("Sign in by External Account");
                }

                var user = new UserDbModel
                {
                    UserName = externalInfo.Principal.FindFirstValue(ClaimTypes.Email),
                    Email = externalInfo.Principal.FindFirstValue(ClaimTypes.Email)
                };

                var result = await _userManager.CreateAsync(user);

                if (!result.Succeeded) return BadRequest("User create error");

                await _userManager.AddLoginAsync(user, externalInfo);
                await _signInManager.SignInAsync(user, false);

                return Ok("Sign in by External Account and create new account");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
