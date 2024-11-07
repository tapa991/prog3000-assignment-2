using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrick_T_Assignment_2.Data;
using Patrick_T_Assignment_2.Models.Entities;

namespace Patrick_T_Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : Controller
    {
        private readonly AppDbContext _context;

        public ImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Image>>> GetAllImages()
        {
            // Retrieve all images from the database
            var images = await _context.Images
                                        .ToListAsync();

            // Return a 200 OK response with the list of images
            return Ok(images);
        }
    }
}
