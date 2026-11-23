using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_91_EF04.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(a => a.AccountNumber);

            builder.Property(a => a.AccountNumber).IsRequired().HasMaxLength(20);
            builder.Property(a => a.AccountType).IsRequired();
            builder.Property(a => a.CurrentBalance).IsRequired().HasPrecision(18, 2);
            builder.Property(a => a.OpeningDate).IsRequired().HasColumnType("date");
            builder.Property(a => a.BranchCode).IsRequired().HasMaxLength(10);

            builder.HasOne(a => a.Branch)
                .WithMany(b => b.Accounts)
                .HasForeignKey(a => a.BranchCode)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}