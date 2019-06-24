using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kazidocs2.Models;

namespace Kazidocs2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTokensController : ControllerBase
    {
        private readonly KazidocsContext _context;

        public UserTokensController(KazidocsContext context)
        {
            _context = context;
        }

        // GET: api/UserTokens
        [HttpGet]
        public IEnumerable<UserTokens> GetTokens()
        {
            return _context.Tokens;
        }

        // GET: api/UserTokens/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserTokens([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTokens = await _context.Tokens.FindAsync(id);

            if (userTokens == null)
            {
                return NotFound();
            }

            return Ok(userTokens);
        }

        // PUT: api/UserTokens/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserTokens([FromRoute] string id, [FromBody] UserTokens userTokens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userTokens.Id)
            {
                return BadRequest();
            }

            _context.Entry(userTokens).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserTokensExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UserTokens
        [HttpPost]
        public async Task<IActionResult> PostUserTokens([FromBody] UserTokens userTokens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tokens.Add(userTokens);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserTokens", new { id = userTokens.Id }, userTokens);
        }

        // DELETE: api/UserTokens/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserTokens([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userTokens = await _context.Tokens.FindAsync(id);
            if (userTokens == null)
            {
                return NotFound();
            }

            _context.Tokens.Remove(userTokens);
            await _context.SaveChangesAsync();

            return Ok(userTokens);
        }

        private bool UserTokensExists(string id)
        {
            return _context.Tokens.Any(e => e.Id == id);
        }
    }
}