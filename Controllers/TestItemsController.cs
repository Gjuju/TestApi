#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApi.Models;

namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestItemsController : ControllerBase
    {
        private readonly TestContext _context;

        public TestItemsController(TestContext context)
        {
            _context = context;
        }

        // GET: api/TestItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItems()
        {
            return await _context.TestItems.ToListAsync();
        }

        // GET: api/TestItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TestItem>> GetTestItem(long id)
        {
            var TestItem = await _context.TestItems.FindAsync(id);

            if (TestItem == null)
            {
                return NotFound();
            }
         
            return TestItem;
        }

        // GET: api/TestItems/name/"Luke"
        [HttpGet("name/{name:alpha}")]
        public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItemByName(string name)
        {
            var TestItem = await _context.TestItems.ToListAsync();


            var filtered = new List<TestItem>();

            foreach (var item in TestItem)
            {
                if (item.Name == name)
                {
                    filtered.Add(item);
                }
            }

            if (filtered.Count == 0)
            {
                return NotFound();
            }

            return filtered;
        }

        // GET: api/TestItems/age/5
        [HttpGet("age/{age:int}")]
        public async Task<ActionResult<IEnumerable<TestItem>>> GetTestItemByAge(long age)
        {
            var TestItem = await _context.TestItems.ToListAsync();


            var filtered = new List<TestItem>();

            foreach (var item in TestItem)
            {
                if (item.Age == age)
                {
                    filtered.Add(item);
                }
            }

            if (filtered.Count == 0)
            {
                return NotFound();
            }

            return filtered;
        }

        // PUT: api/TestItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTestItem(long id, TestItem TestItem)
        {
            if (id != TestItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(TestItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestItemExists(id))
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

        // POST: api/TestItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TestItem>> PostTestItem(TestItem TestItem)
        {
            _context.TestItems.Add(TestItem);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetTestItem", new { id = TestItem.Id }, TestItem);
            return CreatedAtAction(nameof(GetTestItem), new { id = TestItem.Id }, TestItem);
        }

        // DELETE: api/TestItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTestItem(long id)
        {
            var TestItem = await _context.TestItems.FindAsync(id);
            if (TestItem == null)
            {
                return NotFound();
            }

            _context.TestItems.Remove(TestItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TestItemExists(long id)
        {
            return _context.TestItems.Any(e => e.Id == id);
        }
    }
}
