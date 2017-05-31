using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web.Mvc;
using VideothekData;
using VideothekData.Models;
using Videothek.ViewModels;
using System.Threading.Tasks;

namespace Videothek.Controllers
{
    [Authorize(Roles = "Employee, Role1")]
    public class CustomerController : Controller
    {
        private readonly VideothekContext _context;
        public CustomerController()
        {
            _context = new VideothekContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Customer
        public async Task<ActionResult> Index()
        {
            List<Customer> customers = await _context.Customers
                                                     .Include(c => c.Membership).ToListAsync();

            return View(customers);
        }

        public async Task<ActionResult> Details(int id)
        {
            //using System.Data.Entity
            Customer customer = await _context.Customers.Include(c => c.Membership)
                                                        .SingleOrDefaultAsync(c => c.Id == id);

            return View(customer);
        }

        public async Task<ActionResult> New()
        {
            CustomerFormViewModel model = new CustomerFormViewModel
            {
                Memberships = await _context.Memberships.ToListAsync()
            };

            return View("Form", model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            CustomerFormViewModel model = new CustomerFormViewModel
            {
                Customer = await _context.Customers.FindAsync(id),
                Memberships = await _context.Memberships.ToListAsync()
            };

            return View("Form", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                CustomerFormViewModel model = new CustomerFormViewModel
                {
                    Customer = customer,
                    Memberships = await _context.Memberships.ToListAsync()
                };
                return View("Form", model);
            }

            if (customer.Id == null)
                _context.Customers.Add(customer);
            else
                _context.Entry(customer).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}