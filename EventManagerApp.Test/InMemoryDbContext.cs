using Microsoft.EntityFrameworkCore;
using MonApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonApp.Test
{
    internal class InMemoryDbContext : AppDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseInMemoryDatabase("bdd");
        }

        public void EnsureDeleted()
        {
            this.Database.EnsureDeleted();
        }
    }
}
