using delivery_server_api.Contexts;
using delivery_server_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemController : ControllerBase
    {
        private readonly FoodDBContext _dbContext;

        public FoodItemController(FoodDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [Route("addItem")]
        [HttpPost]
        public async Task<IActionResult> PostNewItem([FromForm] FoodFormModel model)
        {
            try
            {
                var fileName = model.Image.FileName;
                var filePath = Path.Combine(@"D:\Programming\Projects\Images\", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Image.CopyToAsync(stream);
                }

                var image = new Image { ImageName = model.Image.FileName, LocalPath = filePath };
                var foodItem = new FoodItem { Title = model.Title, Price = model.Price, Image = image };
                await _dbContext.FoodItems.AddAsync(foodItem);
                await _dbContext.SaveChangesAsync();

                return Ok("File saved and adding to DB");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("viewItem")]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _dbContext.FoodItems.Include(x => x.Image).ToListAsync();
                var itemList = new List<FoodViewModel>();

                foreach (var item in items)
                {
                    var image = File(await System.IO.File.ReadAllBytesAsync(item.Image.LocalPath), "image/jpeg");
                    var itemResult = new FoodViewModel { Id = item.FoodId, Title = item.Title, Price = item.Price, Image = image };
                    itemList.Add(itemResult);
                }

                return Ok(itemList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("viewItem/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            try
            {
                var item = await _dbContext.FoodItems.Include(x => x.Image).SingleOrDefaultAsync(x => x.FoodId == id);
                var image = File(await System.IO.File.ReadAllBytesAsync(item.Image.LocalPath), "image/jpeg");
                var itemResult = new FoodViewModel { Id = item.FoodId, Title = item.Title, Price = item.Price, Image = image };
                return Ok(itemResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
