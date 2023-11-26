/* Author:  Lindy Stewart
 * Editor:  Eric Robinson L00709820
 * Date:    11/25/23
 * Course:  Lane Community College CS234 Advanced Programming: C# (.NET)
 * Lab:     6 
 * Purpose: 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MMABooksEFClasses.Models;

namespace MMABooksRESTAPI.Controllers
{
    //the Route is the URL https://localhost:44321/api/states
    [Route("api/[controller]")]
    [ApiController]

    //StatesController is derived from ControllerBase (some class that does some things that was written by Mcrosoft)
    public class StatesController : ControllerBase
    {
        //in order to connect to the database is needs an instance of our MMABooksContext object
        //instance varibale _context 
        private readonly MMABooksContext _context;

        //the constructor will assign the value. This will get added by a dependency injection
        //which we won't gove over now. Just know that the context is being added here. 
        public StatesController(MMABooksContext context)
        {
            _context = context;
        }

        // GET: api/States
        //HttpGet is the HTTP Protocol is used to retrieve the data over the internet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<State>>> GetStates()
        // Returns all states.
        {
            //the body of the methods should look familiar and be somewhat understandable. 

            if (_context.States == null)
          {
              return NotFound();
          }
          //what may not seem familiar is some terms like Async. Lets go back to the slides
          //and review some of the terms. 
            return await _context.States.ToListAsync();
        }

        //notice with the parameter it will have the url..with the api/controller/parameter value
        //See example api/States/5
        // GET: api/States/5
        [HttpGet("{id}")]
        public async Task<ActionResult<State>> GetState(string id)
        // Returns 1 state.
        {
            if (_context.States == null)
          {
              return NotFound();
          }
            var state = await _context.States.FindAsync(id);

            if (state == null)
            {
                return NotFound();
            }

            return state;
        }

        // PUT: api/States/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]

        public async Task<IActionResult> PutState(string id, State state)
        // Updates 1 state record (requires 2 parameters).
        {
            // If the id is not found then it will return a BadRequest message.
            if (id != state.StateCode)
            {
                return BadRequest();
            }

            // This changes the states in the _context object.
            _context.Entry(state).State = EntityState.Modified;

            // Then it will will save it in the database.
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            // What does this mean?
            // See: https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.controllerbase.nocontent?view=aspnetcore-7.0
            return NoContent();
        }

        // POST: api/States
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // Notice that the HTTP has POST
        [HttpPost]

        // Notice that we have a parameter here. This is because we are going to be adding
        // a state to the state table. It will be used as part of the body of this post request
        // in Json format.
        public async Task<ActionResult<State>> PostState(State state)
        // Creates 1 state record.
        {
          if (_context.States == null)
          {
              return Problem("Entity set 'MMABooksContext.States'  is null.");
          }
          // Using the _context object, States collection, and adding a state.
            _context.States.Add(state);
            try
            {
                // We use await and save changes asyncronously.
                await _context.SaveChangesAsync();
            }

            // And if there is an exception, like if we are wanting to insert a state that already exsists
            // so we will have it catch that error and return the Conflict() method.
            catch (DbUpdateException)
            {
                if (StateExists(state.StateCode))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            // If it is created we will get back is a State object, and that state object will end up 
            // with the primary id to = statecode and state equal to itself.
            return CreatedAtAction("GetState", new { id = state.StateCode }, state);
        }

        // DELETE: api/States/5
        // Notice that with the URL it includes the {id}. You need to have this in order to specify which state you want
        // to delete. 
        [HttpDelete("{id}")]

        //notice the DeleteState method had id as its parameter.
        public async Task<IActionResult> DeleteState(string id)
        // Deletes 1 state.
        {

            // In the body of the method we use await since we are using async so it looks like we are using synchonous code when we really are not.
            // _context.States.FindAsync(id) to find the thing based on the id, if it doesn;t find it, it will return NotFound()
            // otherwise it will Remove(state), then calls SaveChangesAsync();
            if (_context.States == null)
            {
                return NotFound();
            }
            var state = await _context.States.FindAsync(id);
            if (state == null)
            {
                return NotFound();
            }

            _context.States.Remove(state);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StateExists(string id)
        {
            return (_context.States?.Any(e => e.StateCode == id)).GetValueOrDefault();
        }

    } // end class StatesController : ControllerBase
} // end namespace MMABooksRESTAPI.Controllers
