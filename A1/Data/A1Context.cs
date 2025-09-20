using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using A1.Models;

namespace A1.Data
{
    public class A1Context : DbContext
    {
        public A1Context (DbContextOptions<A1Context> options)
            : base(options)
        {
        }

        public DbSet<A1.Models.Usuario> Usuario { get; set; } = default!;
    }
}
