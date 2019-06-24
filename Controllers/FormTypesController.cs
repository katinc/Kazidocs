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
    public class FormTypesController : ControllerBase
    {
        private readonly KazidocsContext _context;

        //***Methods for Form Type***//
        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormTypes>>> GetFormTypes(long id)
        {
            return await _context.FormTypes.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<FormTypes>> GetFormType(long id)
        {
            var formType = await _context.FormTypes.FindAsync(id);

            if (formType == null)
            {
                return NotFound();
            }

            return formType;
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<FormTypes>> AddFormType(FormTypes item)
        {
            _context.FormTypes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddFormType), new { id = item.Id }, item);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormTypes(long id, FormTypes formTypes)
        {
            if (id != formTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(formTypes).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormType(long id)
        {
            var todoItem = await _context.FormTypes.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.FormTypes.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /////Methods for FormValues/////
        ///
    }
}