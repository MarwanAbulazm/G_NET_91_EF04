using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;

namespace G_NET_91_EF04.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>().HasData(
                new Manager { Id = 1, FullName = "Khaled Mostafa", Email = "khaled.mostafa@nationalbank.com", PhoneNumber = "01001234567", HireDate = new DateTime(2015, 6, 1) },
                new Manager { Id = 2, FullName = "Laila Samir", Email = "laila.samir@nationalbank.com", PhoneNumber = "01007654321", HireDate = new DateTime(2018, 9, 15) }
            );

            modelBuilder.Entity<Branch>().HasData(
                new Branch { Code = "CAI-01", Name = "Cairo Main Branch", Address = "10 Tahrir Square, Cairo", PhoneNumber = "0225551001", ManagerId = 1 },
                new Branch { Code = "ALX-01", Name = "Alexandria Branch", Address = "25 Corniche Road, Alexandria", PhoneNumber = "0335551002", ManagerId = 2 }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, FullName = "Ahmed Ali", NationalId = "29001011234567", DateOfBirth = new DateTime(1990, 1, 1), Email = "ahmed.ali@example.com", PhoneNumber = "01011112222", Address = "15 Nasr City, Cairo", CustomerType = CustomerType.Individual },
                new Customer { Id = 2, FullName = "Nile Trading LLC", NationalId = "30005551234567", DateOfBirth = new DateTime(2000, 1, 1), Email = "info@niletrading.com", PhoneNumber = "01233334444", Address = "8 Port Said St, Alexandria", CustomerType = CustomerType.Business }
            );

            modelBuilder.Entity<Account>().HasData(
                new Account { AccountNumber = "2001-CUR", AccountType = AccountType.Current, CurrentBalance = 15400.00m, OpeningDate = new DateTime(2022, 3, 1), BranchCode = "CAI-01" },
                new Account { AccountNumber = "3000-BUS", AccountType = AccountType.Business, CurrentBalance = 120000.00m, OpeningDate = new DateTime(2021, 7, 15), BranchCode = "ALX-01" }
            );

            modelBuilder.Entity<CustomerAccount>().HasData(
                new CustomerAccount { CustomerId = 1, AccountNumber = "2001-CUR", OwnershipType = OwnershipType.Primary, OwnershipStartDate = new DateTime(2022, 3, 1), AccountStatus = AccountStatus.Active },
                new CustomerAccount { CustomerId = 2, AccountNumber = "3000-BUS", OwnershipType = OwnershipType.Primary, OwnershipStartDate = new DateTime(2021, 7, 15), AccountStatus = AccountStatus.Active }
            );
        }
    }
}
