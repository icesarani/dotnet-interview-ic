using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    [Route("api/todolist/{idList:long}/todoitem")]
    [ApiController]
    public class TodoItemController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemController(TodoContext context) { 
            _context = context;
        }

        // GET: api/<TodoItemController>
        //[HttpGet]
        //public async Task<ActionResult<TodoList>> Get(long idList)
        //{


        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<TodoItemController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> Get(long id, long idList)
        {
            TodoList? list = await _context.TodoList.Include(l => l.Items).FirstOrDefaultAsync(l => l.Id == idList);

            if (list == null)
                return NotFound();

            TodoItem? item = list.Items.FirstOrDefault(i => i.Id == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST api/<TodoItemController>
        [HttpPost]
        public async Task<ActionResult<TodoItem>> Post([FromBody] TodoItem item, long idList)
        {
            if (_context.TodoList == null)
                return Problem("Entity set 'TodoContext.TodoList'  is null.");

            TodoList? list = await _context.TodoList.FindAsync(idList);

            if (list == null)
                return NotFound();

            list.Items.Add(item);
            _context.Update(list);

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                return Problem("Error saving in db.");
            }

            return CreatedAtAction("", new { id = item.Id }, item);
        }

        // PUT api/<TodoItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/<TodoItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
