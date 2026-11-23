using G_NET_91_EF04.Data;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_NET_91_EF04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BankContext context = new BankContext();

            bool exit = false;

            while (exit == false)
            {
                ShowMenu();
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCustomer(context);
                        break;
                    case "2":
                        OpenAccount(context);
                        break;
                    case "3":
                        UpdateAccountStatus(context);
                        break;
                    case "4":
                        RemoveAccountFromCustomer(context);
                        break;
                    case "5":
                        ListCustomers(context);
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        ShowError("Invalid choice. Please enter a number between 0 and 5.");
                        break;
                }

                if (exit == false)
                {
                    PressAnyKey();
                }
            }

            context.Dispose();
            Console.WriteLine("Connection closed. Goodbye!");
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("    National Bank - Management");
            Console.WriteLine("========================================");
            Console.WriteLine("  1) Add a new Customer");
            Console.WriteLine("  2) Open a new Account for a Customer");
            Console.WriteLine("  3) Update Account Status (Active / Closed)");
            Console.WriteLine("  4) Remove an Account from a Customer");
            Console.WriteLine("  5) List all Customers (with accounts)");
            Console.WriteLine("  0) Exit");
            Console.WriteLine("----------------------------------------");
            Console.Write("  Enter choice: ");
        }

        static void AddCustomer(BankContext context)
        {
            Console.WriteLine("--- Add New Customer ---");

            string fullName = ReadRequired("Full Name      : ", 100);

            string nationalId = ReadDigits("National ID    : ", 10, 14);
            bool nationalIdExists = context.Customers.Any(c => c.NationalId == nationalId);
            if (nationalIdExists)
            {
                ShowError("A customer with this National ID already exists.");
                return;
            }

            DateTime dateOfBirth = ReadDateOfBirth("Date of Birth  : (yyyy-MM-dd) ");
            string email = ReadEmail("Email          : ");
            string phone = ReadDigits("Phone          : ", 7, 15);
            string address = ReadRequired("Address        : ", 200);

            Console.WriteLine("Customer Type:");
            Console.WriteLine("    1) Individual");
            Console.WriteLine("    2) Business");
            int typeChoice = ReadNumber("  Choice: ", 1, 2);

            Customer customer = new Customer();
            customer.FullName = fullName;
            customer.NationalId = nationalId;
            customer.DateOfBirth = dateOfBirth;
            customer.Email = email;
            customer.PhoneNumber = phone;
            customer.Address = address;
            customer.CustomerType = (CustomerType)typeChoice;

            context.Customers.Add(customer);
            context.SaveChanges();

            Console.WriteLine();
            ShowSuccess("Customer created successfully. CustomerId = " + customer.Id);
        }

        static void OpenAccount(BankContext context)
        {
            Console.WriteLine("--- Open New Account ---");

            string accountNumber = ReadRequired("Account Number : ", 20);
            bool accountExists = context.Accounts.Any(a => a.AccountNumber == accountNumber);
            if (accountExists)
            {
                ShowError("Account '" + accountNumber + "' already exists.");
                return;
            }

            Console.WriteLine("Account Type:");
            Console.WriteLine("    1) Savings");
            Console.WriteLine("    2) Current");
            Console.WriteLine("    3) Business");
            int accountTypeChoice = ReadNumber("  Choice: ", 1, 3);

            string branchCode = ReadRequired("Branch Code    : ", 10);
            int customerId = ReadNumber("Customer Id    : ", 1, int.MaxValue);

            Console.WriteLine("Ownership Role:");
            Console.WriteLine("    1) Primary");
            Console.WriteLine("    2) CoHolder");
            int ownershipChoice = ReadNumber("  Choice: ", 1, 2);

            ShowInfo("Validating branch '" + branchCode + "' and customer #" + customerId + "...");

            Branch? branch = context.Branches.FirstOrDefault(b => b.Code == branchCode);
            if (branch == null)
            {
                ShowError("Branch '" + branchCode + "' does not exist.");
                return;
            }

            Customer? customer = context.Customers.Find(customerId);
            if (customer == null)
            {
                ShowError("Customer #" + customerId + " does not exist.");
                return;
            }

            Account account = new Account();
            account.AccountNumber = accountNumber;
            account.AccountType = (AccountType)accountTypeChoice;
            account.CurrentBalance = 0;
            account.OpeningDate = DateTime.Today;
            account.BranchCode = branch.Code;

            CustomerAccount customerAccount = new CustomerAccount();
            customerAccount.CustomerId = customerId;
            customerAccount.AccountNumber = accountNumber;
            customerAccount.OwnershipType = (OwnershipType)ownershipChoice;
            customerAccount.OwnershipStartDate = DateTime.Today;
            customerAccount.AccountStatus = AccountStatus.Active;

            context.Accounts.Add(account);
            context.CustomerAccounts.Add(customerAccount);
            context.SaveChanges();

            ShowSuccess("Account '" + accountNumber + "' created and linked to customer " + customerId + " as " + customerAccount.OwnershipType + " owner.");
        }

        static void UpdateAccountStatus(BankContext context)
        {
            Console.WriteLine("--- Update Account Status ---");

            string accountNumber = ReadRequired("Account Number : ", 20);
            int customerId = ReadNumber("Customer Id    : ", 1, int.MaxValue);

            Console.WriteLine("New Status:");
            Console.WriteLine("    1) Active");
            Console.WriteLine("    2) Closed");
            int statusChoice = ReadNumber("  Choice: ", 1, 2);

            CustomerAccount? customerAccount = context.CustomerAccounts
                .FirstOrDefault(ca => ca.CustomerId == customerId && ca.AccountNumber == accountNumber);

            if (customerAccount == null)
            {
                ShowError("Account '" + accountNumber + "' is not linked to customer " + customerId + ".");
                return;
            }

            customerAccount.AccountStatus = (AccountStatus)statusChoice;
            context.SaveChanges();

            ShowSuccess("Status updated to " + customerAccount.AccountStatus + ".");
        }

        static void RemoveAccountFromCustomer(BankContext context)
        {
            Console.WriteLine("--- Remove Account From Customer ---");

            string accountNumber = ReadRequired("Account Number : ", 20);
            int customerId = ReadNumber("Customer Id    : ", 1, int.MaxValue);

            CustomerAccount? customerAccount = context.CustomerAccounts
                .FirstOrDefault(ca => ca.CustomerId == customerId && ca.AccountNumber == accountNumber);

            if (customerAccount == null)
            {
                ShowError("Account '" + accountNumber + "' is not linked to customer " + customerId + ".");
                return;
            }

            bool hasOtherOwners = context.CustomerAccounts
                .Any(ca => ca.AccountNumber == customerAccount.AccountNumber && ca.CustomerId != customerId);

            context.CustomerAccounts.Remove(customerAccount);

            if (hasOtherOwners == false)
            {
                Account? account = context.Accounts.Find(customerAccount.AccountNumber);
                if (account != null)
                {
                    context.Accounts.Remove(account);
                }
            }

            context.SaveChanges();

            Console.WriteLine();
            ShowSuccess("  Ownership link deleted.");

            if (hasOtherOwners == false)
            {
                ShowInfo("      That was the last owner - account '" + customerAccount.AccountNumber + "' was also removed.");
            }
        }

        static void ListCustomers(BankContext context)
        {
            List<Customer> customers = context.Customers
                .AsNoTracking()
                .Include(c => c.CustomerAccounts)
                    .ThenInclude(ca => ca.Account)
                        .ThenInclude(a => a.Branch)
                .OrderBy(c => c.Id)
                .ToList();

            Console.WriteLine("--- All Customers ---");
            Console.WriteLine();

            if (customers.Count == 0)
            {
                Console.WriteLine("  (no customers)");
                return;
            }

            foreach (Customer customer in customers)
            {
                Console.WriteLine($"  #{customer.Id} {customer.FullName} ({customer.CustomerType})");

                if (customer.CustomerAccounts.Count == 0)
                {
                    Console.WriteLine("      (no accounts)");
                }

                foreach (CustomerAccount link in customer.CustomerAccounts)
                {
                    Account account = link.Account;
                    Console.WriteLine($"      {account.AccountNumber,-10}{account.AccountType,-10}Balance: {account.CurrentBalance,12:N2}   {link.OwnershipType,-9}{link.AccountStatus,-8}@ {account.Branch.Name}");
                }
            }
        }

        static string ReadText(string message)
        {
            Console.Write(message);
            string? input = Console.ReadLine();

            if (input == null)
            {
                return "";
            }

            return input.Trim();
        }

        static string ReadRequired(string message, int maxLength)
        {
            while (true)
            {
                string text = ReadText(message);

                if (text == "")
                {
                    ShowError("This field is required.");
                }
                else if (text.Length > maxLength)
                {
                    ShowError("Maximum length is " + maxLength + " characters.");
                }
                else
                {
                    return text;
                }
            }
        }

        static string ReadDigits(string message, int minLength, int maxLength)
        {
            while (true)
            {
                string text = ReadText(message);

                if (text == "")
                {
                    ShowError("This field is required.");
                }
                else if (IsDigitsOnly(text) == false)
                {
                    ShowError("Please enter digits only.");
                }
                else if (text.Length < minLength || text.Length > maxLength)
                {
                    ShowError("Please enter between " + minLength + " and " + maxLength + " digits.");
                }
                else
                {
                    return text;
                }
            }
        }

        static bool IsDigitsOnly(string text)
        {
            foreach (char c in text)
            {
                if (char.IsDigit(c) == false)
                {
                    return false;
                }
            }

            return true;
        }

        static string ReadEmail(string message)
        {
            while (true)
            {
                string text = ReadRequired(message, 200);

                if (text.Contains("@") && text.Contains(".") && text.Contains(" ") == false)
                {
                    return text;
                }

                ShowError("Invalid email. Example: name@example.com");
            }
        }

        static DateTime ReadDateOfBirth(string message)
        {
            while (true)
            {
                string text = ReadText(message);
                DateTime date;

                if (DateTime.TryParse(text, out date) == false)
                {
                    ShowError("Invalid date. Please use the format yyyy-MM-dd.");
                }
                else if (date > DateTime.Today)
                {
                    ShowError("Date of birth cannot be in the future.");
                }
                else if (date.Year < 1900)
                {
                    ShowError("Date of birth is too old.");
                }
                else
                {
                    return date;
                }
            }
        }

        static int ReadNumber(string message, int min, int max)
        {
            while (true)
            {
                string text = ReadText(message);
                int number;

                if (int.TryParse(text, out number) && number >= min && number <= max)
                {
                    return number;
                }

                ShowError("Invalid input. Please enter a number between " + min + " and " + max + ".");
            }
        }

        static void PressAnyKey()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the menu...");
            Console.ReadKey(true);
        }

        static void ShowSuccess(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowInfo(string message)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        static void ShowError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}