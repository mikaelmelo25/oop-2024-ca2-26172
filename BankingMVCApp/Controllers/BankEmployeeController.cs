
//Name:  Mikael Melo
//student number: 26172
using BankingMVC.Data;
using BankingMVC.Data.Entities;
using BankingSharedLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingMVCApp.Controllers
{
    public class BankEmployeeController : Controller
    {
        private readonly BankingDbContext _dbContext;

        public BankEmployeeController(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var customers = _dbContext.Customers
                .Include(c => c.CurrentAccount)
                    .ThenInclude(ca => ca.Transactions)
                    .ThenInclude(cat => cat.Transaction)
                .Include(c => c.SavingsAccount)
                    .ThenInclude(sa => sa.Transactions)
                    .ThenInclude(sat => sat.Transaction)
                .ToList();

            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerEntity customer)
        {
            if (ModelState.IsValid)
            {
                var bankEmployee = new BankEmployee("", "", "", "A1234");
                customer.AccountNumber = bankEmployee.GenerateAccountNumber(customer.FirstName, customer.LastName);
                customer.Pin = bankEmployee.GeneratePin(customer.FirstName, customer.LastName);
                customer.SavingsAccount = new SavingsAccountEntity(customer.AccountNumber, customer.Pin);
                //customer.CurrentAccount = new CurrentAccountEntity(customer.AccountNumber, customer.Pin);
                customer.CurrentAccount = new CurrentAccountEntity();
                customer.CurrentAccount.AccountNumber = bankEmployee.GenerateAccountNumber(customer.FirstName, customer.LastName);
                customer.CurrentAccount.Pin = bankEmployee.GeneratePin(customer.FirstName, customer.LastName);

                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(customer);
        }

        public IActionResult DeleteCustomer(int id)
        {
            var customer = _dbContext.Customers
                .Include(c => c.SavingsAccount)
                .Include(c => c.CurrentAccount)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null || customer.SavingsAccount.Balance != 0 || customer.CurrentAccount.Balance != 0)
            {
                return NotFound();
            }

            return View(customer);
        }

        [HttpPost, ActionName("DeleteCustomer")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCustomerConfirmed(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            _dbContext.Customers.Remove(customer);
            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult CreateTransaction(int id)
        {
            var customer = _dbContext.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            ViewBag.Customer = customer;
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTransaction(int id, string accountType, string action, decimal amount)
        {
            var customer = _dbContext.Customers
                .Include(c => c.SavingsAccount)
                .Include(c => c.CurrentAccount)
                .FirstOrDefault(c => c.Id == id);

            if (customer == null)
            {
                return NotFound();
            }

            var bankEmployee = new BankEmployee("", "", "", "A1234");
            var transaction = bankEmployee.CreateTransaction(customer, accountType, action, amount);

            if (accountType.Equals("Savings", StringComparison.OrdinalIgnoreCase))
            {
                var transactionEntity = new TransactionEntity(
                    transaction.Date,
                    transaction.Action,
                    transaction.Amount,
                    transaction.FinalBalance
                );

                _dbContext.Transactions.Add(transactionEntity);

                var transactionRecordEntity = new SavingsAccountTransactionEntity
                {
                    SavingsAccount = customer.SavingsAccount,
                    Transaction = transactionEntity
                };

                _dbContext.SavingsAccountTransactions.Add(transactionRecordEntity);
            }
            else if (accountType.Equals("Current", StringComparison.OrdinalIgnoreCase))
            {
                var transactionEntity = new TransactionEntity(
                    transaction.Date,
                    transaction.Action,
                    transaction.Amount,
                    transaction.FinalBalance
                );

                _dbContext.Transactions.Add(transactionEntity);

                var transactionRecordEntity = new CurrentAccountTransactionEntity
                {
                    CurrentAccount = customer.CurrentAccount,
                    Transaction = transactionEntity
                };

                _dbContext.CurrentAccountTransactions.Add(transactionRecordEntity);
            }
            else
            {
                ModelState.AddModelError("", "Invalid account type.");
            }

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}