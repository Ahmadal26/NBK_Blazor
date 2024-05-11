using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NBK_API.Models.Entites.Users;
using NBK_API.Models.Entites;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("[controller]")]

public class CustomersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: /Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        return await _context.Customers.ToListAsync();
    }

    // GET: /Customers/{id}
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

    // POST: /Customers
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCustomer), new { id = customer.CustomerNumber }, customer);
    }

    // PUT: /Customers/{id}

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateCustomer(int id, Customer customer)
    {
        if (id != customer.CustomerNumber)
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
            if (!_context.Customers.Any(e => e.CustomerNumber == id))
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

    // DELETE: /Customers/{id}
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
