using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_91_EF04.Configurations
{
    public class CustomerAccountConfiguration : IEntityTypeConfiguration<CustomerAccount>
    {
        public void Configure(EntityTypeBuilder<CustomerAccount> builder)
        {
            builder.HasKey(ca => new { ca.CustomerId, ca.AccountNumber });

            builder.Property(ca => ca.AccountNumber).HasMaxLength(20);
            builder.Property(ca => ca.OwnershipType).IsRequired();
            builder.Property(ca => ca.OwnershipStartDate).IsRequired().HasColumnType("date");
            builder.Property(ca => ca.AccountStatus).IsRequired();

            builder.HasOne(ca => ca.Customer)
                .WithMany(c => c.CustomerAccounts)
                .HasForeignKey(ca => ca.CustomerId)
                .IsRequired();

            builder.HasOne(ca => ca.Account)
                .WithMany(a => a.CustomerAccounts)
                .HasForeignKey(ca => ca.AccountNumber)
                .IsRequired();
        }
    }
}