using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private static List<Users> users = new List<Users>
        {
                new Users {
                    Id = 1,
                    FirstName ="Oliver",
                    LastName="Mustonen",
                    Email="oliver.mustonen@gmail.com"
                },
                new Users { 
                    Id=2,
                    FirstName="Axel",
                    LastName="Olsson",
                    Email ="axe.olsson@gmail.com",
                }
        };
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Users>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Users>>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return BadRequest("User not found.");
            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<List<Users>>> AddUser(Users user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<Users>>> UpdateUser(Users request)
        {
            var dbUser = await _context.Users.FindAsync(request.Id);
            if (dbUser == null)
                return BadRequest("User not found.");

            dbUser.FirstName = request.FirstName;
            dbUser.LastName = request.LastName;
            dbUser.Email = request.Email;

            await _context.SaveChangesAsync();

            return Ok(await _context.Users.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Users>>> Delete(int id)
        {
            var dbUser = await _context.Users.FindAsync(id);
            if (dbUser == null)
                return BadRequest("User not found.");

            _context.Users.Remove(dbUser);
            await _context.SaveChangesAsync();
            return Ok(await _context.Users.ToListAsync());
        }


    }
}
