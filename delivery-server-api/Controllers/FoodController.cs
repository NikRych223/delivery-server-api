using delivery_server_api.Contexts;
using delivery_server_api.Models;
using delivery_server_api.Models.FoodModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace delivery_server_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly FoodDBContext _dbContext;

        public FoodController(FoodDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region CREATE

        [Route("item")]
        [HttpPost]
        public async Task<IActionResult> PostNewItem([FromForm] FoodAddModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var fileExtension = model.Image.FileName.Split('.').Last();

                    if (fileExtension != "jpeg" && fileExtension != "jpg" && fileExtension != "png")
                    {
                        return BadRequest("Bad file extension");
                    }

                    var fileName = $"{DateTime.Now:ddMMyyhhmm}-{new Random().Next(100, 999)}.{fileExtension}";
                    var filePath = Path.Combine(@"D:\Programming\Projects\Images\", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.Image.CopyToAsync(stream);
                    }

                    var image = new Image { ImageName = fileName, LocalPath = filePath };
                    var foodItem = new FoodDbModel { Title = model.Title, Price = model.Price, Image = image };
                    await _dbContext.FoodItems.AddAsync(foodItem);
                    await _dbContext.SaveChangesAsync();

                    return Ok("File saved and adding to DB");
                }

                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region READ

        [Route("item")]
        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _dbContext.FoodItems.ToListAsync();

                if (items == null) return NotFound();

                var itemList = new List<FoodViewModel>();

                foreach (var item in items)
                {
                    itemList.Add(new FoodViewModel(item.FoodId, item.Title, item.Price));
                }

                return Ok(itemList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("item/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            try
            {
                var item = await _dbContext.FoodItems.FindAsync(id);

                if (item == null) return NotFound();

                return Ok(new FoodViewModel(item.FoodId, item.Title, item.Price));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("item/image/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetImageByFoodId(Guid id)
        {
            try
            {
                var item = await _dbContext.FoodItems.Include(x => x.Image).SingleOrDefaultAsync(x => x.FoodId == id);

                if (item == null) return NotFound();

                var image = File(await System.IO.File.ReadAllBytesAsync(item.Image.LocalPath), "image/jpeg");

                return image;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        [Route("item/{id}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteItemById(Guid id)
        {
            try
            {
                var item = await _dbContext.FoodItems.Include(x => x.Image).SingleOrDefaultAsync(x => x.FoodId == id);

                if (item == null) return NotFound();

                System.IO.File.Delete(item.Image.LocalPath);
                _dbContext.FoodItems.Remove(item);

                // TODO - remove from Favorite DB - Added by inherite FoodDbModel

                await _dbContext.SaveChangesAsync();

                return Ok("Item remove from DB");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
