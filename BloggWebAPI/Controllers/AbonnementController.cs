using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BloggWebAPI.Models;

namespace BloggWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AbonnementController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AbonnementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/abonnement/{userId}/{bloggId}
        [HttpPost("{userId}/{bloggId}")]
        public async Task<IActionResult> AbonnerPåBlogg(string userId, int bloggId)
        {
            try
            {
                var existingSubscription = await _context.Abonnementer
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.BloggId == bloggId);

                if (existingSubscription != null)
                {
                    return BadRequest("Brukeren abonnerer allerede på denne bloggen.");
                }

                var abonnement = new Abonnement
                {
                    UserId = userId,
                    BloggId = bloggId
                };

                _context.Abonnementer.Add(abonnement);
                await _context.SaveChangesAsync();

                return Ok("Abonnement opprettet suksessfullt.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Feil ved oppretting av abonnement: {ex.Message}");
            }
        }

        // GET: api/abonnement/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAbonnementerForBruker(string userId)
        {
            try
            {
                var abonnementer = await _context.Abonnementer
                    .Where(a => a.UserId == userId)
                    .Include(a => a.Blogg)
                    .ToListAsync();

                var blogger = abonnementer.Select(a => new
                {
                    BloggId = a.Blogg.BloggId,
                    Tittel = a.Blogg.Tittel,
                    Beskrivelse = a.Blogg.Beskrivelse
                }).ToList();

                return Ok(blogger);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Feil ved henting av abonnementer: {ex.Message}");
            }
        }


        // DELETE: api/abonnement/{userId}/{bloggId}
        [HttpDelete("{userId}/{bloggId}")]
        public async Task<IActionResult> AvsluttAbonnement(string userId, int bloggId)
        {
            try
            {
                var abonnement = await _context.Abonnementer
                    .FirstOrDefaultAsync(a => a.UserId == userId && a.BloggId == bloggId);

                if (abonnement == null)
                {
                    return NotFound("Abonnementet ble ikke funnet.");
                }

                _context.Abonnementer.Remove(abonnement);
                await _context.SaveChangesAsync();

                return Ok("Abonnement avsluttet suksessfullt.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Feil ved avslutning av abonnement: {ex.Message}");
            }
        }


    }
}
