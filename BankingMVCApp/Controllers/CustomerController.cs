//Name:  Mikael Melo
//student number: 26172

using BankingMVC.Data;
using BankingSharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BankingMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly BankingDbContext _dbContext;

        public CustomerController(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string firstName, string lastName, string accountNumber, string pin)
        {
            var customer = _dbContext.Customers
                .Include(c => c.SavingsAccount)
                .Include(c => c.CurrentAccount)
                .FirstOrDefault(c => c.FirstName == firstName && c.LastName == lastName && c.AccountNumber == accountNumber);

            if (customer != null && customer.Login(pin))
            {
                return RedirectToAction("AccountDetails", new { id = customer.Id });
            }

            ModelState.AddModelError(string.Empty, "Invalid credentials.");
            return View();
        }

        public IActionResult AccountDetails(int id)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        public IActionResult TransactionHistory(int id, string accountType)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            var transactionHistory = customer.RetrieveTransactionHistory(accountType);
            return View(transactionHistory);
        }

        public IActionResult AddMoney(int id, string accountType)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.AccountType = accountType;
            return View();
        }

        [HttpPost]
        public IActionResult AddMoney(int id, string accountType, decimal amount)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.AddMoney(accountType, amount);
            _dbContext.SaveChanges();
            return RedirectToAction("AccountDetails", new { id = customer.Id });
        }

        public IActionResult SubtractMoney(int id, string accountType)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.AccountType = accountType;
            return View();
        }

        [HttpPost]
        public IActionResult SubtractMoney(int id, string accountType, decimal amount)
        {
            var customer = GetCustomerFromDatabase(id);
            if (customer == null)
            {
                return NotFound();
            }

            if (amount > 0)
            {
                try
                {
                    customer.SubtractMoney(accountType, amount);
                    _dbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    return View(new SubtractMoneyViewModel
                    {
                        AccountType = accountType,
                        Amount = amount
                    });
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Amount must be greater than zero.");
                return View(new SubtractMoneyViewModel
                {
                    AccountType = accountType,
                    Amount = amount
                });
            }

            return RedirectToAction("AccountDetails", new { id = customer.Id });
        }

        private Customer GetCustomerFromDatabase(int id)
        {
            return _dbContext.Customers
                .Include(c => c.SavingsAccount)
                .Include(c => c.CurrentAccount)
                .FirstOrDefault(c => c.Id == id);
        }
    }

    public class SubtractMoneyViewModel
    {
        public string AccountType { get; set; }
        public decimal Amount { get; set; }
    }
}