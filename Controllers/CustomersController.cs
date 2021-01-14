using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CentrumBibliotektet_Uppdaterad.Data;
using CentrumBibliotektet_Uppdaterad.Models;

namespace CentrumBibliotektet_Uppdaterad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly Context _context;

        public CustomersController(Context context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            return await _context.Customers.ToListAsync();
        }

        // GET: api/Customers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
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

        // POST: api/Customers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            
            return customer;
        }

        // metod för att hyra en bok:

    [HttpPost("{customerId}/RentBook/{PublicationId}")]
    public async Task<ActionResult<Customer>> RentBook(int CustomerId, int PublicationId)
        {
            // sök efter första matchande låntagare
            var customer = await _context.Customers.SingleOrDefaultAsync(c => c.CustomerId == CustomerId);

            //om ingen customer hittas visas en notFound sida

            if (customer == null)
            {
                return NotFound("Customer doesn't exist in the system");
            }

            var inventory = await _context.Inventories
                .Include(i => i.Publication)
                .Include(i => i.Loans)
                .Where(i => i.PublicationId == PublicationId)
                .ToListAsync();

            var availableBook = inventory
                .FirstOrDefault(i => i.Available);

            if (availableBook == null)
            {
                return BadRequest("Book isn't available");
            }

            var loan = new Loan()
            {
                CustomerId = CustomerId,
                InventoryId = availableBook.InventoryId,
                RentalDate = DateTime.Now
            };

            _context.Loans.Add(loan);
            await _context.SaveChangesAsync();

            return Ok("Book Rented!");
        }

        // metod för att lämna tillbaka en bok:

        [HttpPost("{CustomerId}/ReturnBook/{inventoryId}")]
        public async Task<ActionResult<Customer>> ReturnBook(int CustomerId, int inventoryId)
        {
            var customer = await _context.Customers
                .Include(c => c.Loans)
                .ThenInclude(c => c.Inventory)
                .SingleOrDefaultAsync(c => c.CustomerId == CustomerId);

            if (customer == null)
            {
                return NotFound("Customer isn't registered in the database");
            }

            var loan = customer.Loans.FirstOrDefault(l => !l.Returned && l.Inventory.InventoryId == inventoryId);

            if (loan == null)
            {
                return BadRequest("Customer didn't rent that book");
            }

            _context.Entry(loan).State = EntityState.Modified;
            loan.ReturnDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return Ok("Book was returned");
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
