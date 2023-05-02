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

        public UserController(FoodDBContext dbContext, UserManager<FoodUserDbModel> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> PostCreateUser()
        {
            return null;
        }
    }
}
