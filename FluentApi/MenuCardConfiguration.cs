﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentApi
{
    public class MenuCardConfiguration : IEntityTypeConfiguration<MenuCard>
    {
        public void Configure(EntityTypeBuilder<MenuCard> builder)
        {
            builder.ToTable("MenuCards")
                .HasKey(c => c.MenuCardId);

            builder.Property(c => c.MenuCardId)
                .ValueGeneratedOnAdd();

            builder.HasMany(c => c.Menus)
                .WithOne(m => m.MenuCard);
        }
    }
}
