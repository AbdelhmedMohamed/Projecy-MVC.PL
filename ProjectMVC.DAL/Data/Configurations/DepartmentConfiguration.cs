﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProjectMVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.DAL.Data.Configurations
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {


            builder.Property(D => D.Id).UseIdentityColumn(10,10);

            builder.HasMany(D => D.employees)
                   .WithOne(E => E.Department)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
