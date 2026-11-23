using System;
using System.Collections.Generic;
using System.Text;
using G_NET_91_EF04.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace G_NET_91_EF04.Configurations
{
    public class ManagerConfiguration : IEntityTypeConfiguration<Manager>
    {
        public void Configure(EntityTypeBuilder<Manager> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.FullName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Email).IsRequired().HasMaxLength(200);
            builder.Property(m => m.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(m => m.HireDate).IsRequired().HasColumnType("date");
        }
    }
}