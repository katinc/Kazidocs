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
    public class DataTypesController : ControllerBase
    {
        private readonly KazidocsContext _context;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataTypes>>> GetDataTypes(long id)
        {
            return await _context.DataTypes.ToListAsync();
        }

        //Get with id
        [HttpGet("{id}")]
        public async Task<ActionResult<DataTypes>> GetDataType(long id)
        {
            var dataTypes = await _context.DataTypes.FindAsync(id);

            if (dataTypes == null)
            {
                return NotFound();
            }

            return dataTypes;
        }

        [HttpPost()]
        public async Task<ActionResult<DataTypes>> AddDataType(DataTypes item)
        {
            _context.DataTypes.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AddDataType), new { id = item.Id }, item);
        }

        //Update
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDataType(long id, DataTypes dataTypes)
        {
            if (id != dataTypes.Id)
            {
                return BadRequest();
            }

            _context.Entry(dataTypes).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDataType(long id)
        {
            var todoItem = await _context.DataTypes.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.DataTypes.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}