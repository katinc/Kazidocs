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
    public class FormFieldsController : ControllerBase
    {
        private readonly KazidocsContext _context;

        //***Methods for Form FIelds***//
        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormFields>>> GetFormFields(long id)
        {
            return await _context.FormFields.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<FormFields>> GetFormField(long id)
        {
            var formField = await _context.FormFields.FindAsync(id);

            if (formField == null)
            {
                return NotFound();
            }

            return formField;
        }

        [HttpPost]
        public async Task<ActionResult<FormFields>> AddFormField(FormFields item)
        {
            _context.FormFields.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddFormField), new { id = item.Id }, item);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormField(long id, FormFields formFields)
        {
            if (id != formFields.Id)
            {
                return BadRequest();
            }

            _context.Entry(formFields).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormField(long id)
        {
            var todoItem = await _context.FormFields.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.FormFields.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}