using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using TodoApi.Domain.Entities;

namespace TodoApi.Infrastructure
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<TodoItem> Todos => Set<TodoItem>();

        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<TodoItem>(e =>
            {
                e.HasKey(x => x.Id);
                e.Property(x => x.Title).IsRequired().HasMaxLength(200);
                e.Property(x => x.Description).HasMaxLength(1000);
                e.Property(x => x.Status).HasConversion<string>();
            });
        }
    }
}
