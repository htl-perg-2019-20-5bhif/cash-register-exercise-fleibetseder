using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Model
{
    public class BackendDataContext :DbContext
    {
        public BackendDataContext(DbContextOptions<BackendDataContext> options)
: base(options)
        { }

        public DbSet<Product> Product { get; set; }

        public DbSet<ReceiptLine> ReceiptLine { get; set; }


        public DbSet<Receipt> Receipt { get; set; }


    }
}
