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
    public class FormNamesController : ControllerBase
    {
        private readonly KazidocsContext _context;

        //***Methods for Form Name***//
        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormNames>>> GetFormNames(long id)
        {
            return await _context.FormNames.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<FormNames>> GetFormName(long id)
        {
            var formName = await _context.FormNames.FindAsync(id);

            if (formName == null)
            {
                return NotFound();
            }

            return formName;
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<FormNames>> AddFormName(FormNames item)
        {
            _context.FormNames.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddFormName), new { id = item.Id }, item);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormName(long id, FormNames formName)
        {
            if (id != formName.Id)
            {
                return BadRequest();
            }

            _context.Entry(formName).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormName(long id)
        {
            var todoItem = await _context.FormNames.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.FormNames.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}