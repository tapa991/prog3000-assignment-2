using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Patrick_T_Assignment_2.Data;
using Patrick_T_Assignment_2.Models.DTOs;
using Patrick_T_Assignment_2.Models.Entities;
using Patrick_T_Assignment_2.Models.Helpers;

namespace Patrick_T_Assignment_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            if (string.IsNullOrEmpty(userDto.Name) || string.IsNullOrEmpty(userDto.Email))
                return BadRequest("Name and Email are required.");

            if (!IsValidEmail(userDto.Email))
                return BadRequest("Invalid email format.");

            if (_context.Users.Any(u => u.Email == userDto.Email))
                return BadRequest("Email must be unique.");

            // Map the DTO to the User entity
            var user = new User
            {
                Id = Guid.NewGuid(), // Generate a new Guid for the user ID
                Name = userDto.Name,
                Email = userDto.Email,
                Images = new List<Image>() // Initialize an empty list for images
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPost("{id}/image")]
        public async Task<IActionResult> AddImageToUser(Guid id, [FromBody] string imageUrl)
        {
            // Find the user by ID
            var user = await _context.Users.Include(u => u.Images).FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Create a new image object
            var image = new Image
            {
                Id = Guid.NewGuid(),
                Url = imageUrl,
                PostingDate = DateTime.UtcNow,
                User = user,
                Tags = new List<Tag>() // Initialize an empty list for tags
            };

            var tags = ImageHelper.GetTags(imageUrl);
            List<Tag> tagObjects = tags.Select(tagName => new Tag 
            {
                Id = Guid.NewGuid(),
                Text = tagName,
                Images = new List<Image>()
            }).ToList();

            foreach (var tag in tagObjects)
            {
                // Check if the tag already exists in the database by its Text (or Name)
                if (!_context.Tags.Any(t => t.Text == tag.Text))
                {
                    // If the tag doesn't exist, add it
                    _context.Tags.Add(tag);
                }
            }

            _context.Images.Add(image);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return Ok(image.Url);
        }

        // Helper method to validate email format
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
