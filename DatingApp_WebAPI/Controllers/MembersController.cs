using DatingApp_WebAPI.Data;
using DatingApp_WebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return Ok(members);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult<AppUser>> GetMember(string Id)
        {
            var member = await context.Users.FindAsync(Id);
            if (member == null)
            {
                return NotFound();
            }
            return Ok(member);
        }
    }
}
