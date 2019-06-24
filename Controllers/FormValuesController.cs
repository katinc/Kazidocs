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
    public class FormValuesController : ControllerBase
    {
        private readonly KazidocsContext _context;

        //***Methods for Form Type***//
        //Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormValues>>> GetFormValues(long id)
        {
            return await _context.FormValues.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<FormValues>> GetFormValue(long id)
        {
            var formValue = await _context.FormValues.FindAsync(id);

            if (formValue == null)
            {
                return NotFound();
            }

            return formValue;
        }

        //Add
        [HttpPost]
        public async Task<ActionResult<FormValues>> AddFormValues(FormValues item)
        {
            _context.FormValues.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddFormValues), new { id = item.Id }, item);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFormValues(long id, FormValues formValues)
        {
            if (id != formValues.Id)
            {
                return BadRequest();
            }

            _context.Entry(formValues).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormValues(long id)
        {
            var todoItem = await _context.FormValues.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.FormValues.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}