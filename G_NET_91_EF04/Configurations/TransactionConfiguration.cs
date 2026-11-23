using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_91_EF04.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(t => t.TransactionNumber);

            builder.Property(t => t.TransactionDate).IsRequired();
            builder.Property(t => t.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(t => t.TransactionType).IsRequired();
            builder.Property(t => t.Note).HasMaxLength(250);
            builder.Property(t => t.AccountNumber).IsRequired().HasMaxLength(20);

            builder.HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .HasForeignKey(t => t.AccountNumber)
                .IsRequired();
        }
    }
}