using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_91_EF04.Configurations
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.FullName).IsRequired().HasMaxLength(100);
            builder.Property(c => c.NationalId).IsRequired().HasMaxLength(14);
            builder.HasIndex(c => c.NationalId).IsUnique();
            builder.Property(c => c.DateOfBirth).IsRequired().HasColumnType("date");
            builder.Property(c => c.Email).IsRequired().HasMaxLength(200);
            builder.Property(c => c.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Address).IsRequired().HasMaxLength(200);
            builder.Property(c => c.CustomerType).IsRequired();
        }
    }
}