﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HonorSystem.sakila;

namespace HonorSystem.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemRequestsApiController : ControllerBase
    {
        private readonly EvildogsContext _context;

        public ItemRequestsApiController(EvildogsContext context)
        {
            _context = context;
        }

        // GET: api/ItemRequestsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Itemrequest>>> GetItemrequests([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var items = await _context.Itemrequests
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(items);
        }

        // GET: api/ItemRequestsApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Itemrequest>> GetItemrequest(int id)
        {
            var itemrequest = await _context.Itemrequests.FindAsync(id);

            if (itemrequest == null)
            {
                return NotFound();
            }

            return itemrequest;
        }

        // PUT: api/ItemRequestsApi/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemrequest(int id, Itemrequest itemrequest)
        {
            if (id != itemrequest.IdItemRequest)
            {
                return BadRequest();
            }

            _context.Entry(itemrequest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemrequestExists(id))
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

        // POST: api/ItemRequestsApi
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Itemrequest>> PostItemrequest(Itemrequest itemrequest)
        {
            _context.Itemrequests.Add(itemrequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItemrequest", new { id = itemrequest.IdItemRequest }, itemrequest);
        }

        // DELETE: api/ItemRequestsApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemrequest(int id)
        {
            var itemrequest = await _context.Itemrequests.FindAsync(id);
            if (itemrequest == null)
            {
                return NotFound();
            }

            _context.Itemrequests.Remove(itemrequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemrequestExists(int id)
        {
            return _context.Itemrequests.Any(e => e.IdItemRequest == id);
        }
    }
}
