using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LikeController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public LikeController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    // POST: api/Like
    [HttpPost]
    public async Task<ActionResult<Like>> PostLike(Like like)
    {
        var existingLike = await _context.Likes
            .FirstOrDefaultAsync(l => l.UserId == like.UserId && (l.PostId == like.PostId || l.KommentarId == like.KommentarId));

        if (existingLike != null)
        {
            return BadRequest("Allerede likt");
        }

        _context.Likes.Add(like);
        await _context.SaveChangesAsync();

        var totalLikes = await _context.Likes.CountAsync(l => l.PostId == like.PostId);
        return Ok(totalLikes);
    }


    // DELETE: api/Like/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteLike(int id)
    {
        var like = await _context.Likes.FindAsync(id);
        if (like == null)
        {
            return NotFound();
        }

        _context.Likes.Remove(like);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // GET: api/Like/Post/5
    [HttpGet("Post/{postId}")]
    public async Task<ActionResult<IEnumerable<Like>>> GetLikesForPost(int postId)
    {
        return await _context.Likes
            .Where(l => l.PostId == postId)
            .ToListAsync();
    }

    // GET: api/Like/Kommentar/5
    [HttpGet("Kommentar/{kommentarId}")]
    public async Task<ActionResult<IEnumerable<Like>>> GetLikesForKommentar(int kommentarId)
    {
        return await _context.Likes
            .Where(l => l.KommentarId == kommentarId)
            .ToListAsync();
    }

}
