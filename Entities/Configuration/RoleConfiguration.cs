﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            //Data Entries via migration
            builder.HasData
                (new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                });
        }
    }
}
