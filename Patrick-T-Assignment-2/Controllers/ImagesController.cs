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

        // GET: api/images/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Image>> GetImageById(Guid id)
        {
            // Retrieve the image by ID, including tags if needed
            var image = await _context.Images
                                       .FirstOrDefaultAsync(i => i.Id == id);

            // If the image is not found, return 404
            if (image == null)
            {
                return NotFound(new { Message = "Image not found" });
            }

            // Return the image if found
            return Ok(image);
        }
    }
}
