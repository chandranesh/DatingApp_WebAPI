using DatingApp_WebAPI.Data;
using DatingApp_WebAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp_WebAPI.Controllers
{    
    public class MembersController(AppDbContext context) : BaseApiController
    {
        [HttpGet]//api/members
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.Users.ToListAsync();
            return Ok(members);
        }
        
        [Authorize]
        [HttpGet("{Id}")]//api/members/3
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
