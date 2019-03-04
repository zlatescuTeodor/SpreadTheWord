using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpreadTheWord.Models
{
    public class Paper
    {
        public Guid PaperId { set; get; }
        public string Title{ set; get; }
        public string Content { set; get; }
    }

    public class PaperContext : DbContext
    {
        public PaperContext(DbContextOptions<PaperContext> options)
           : base(options)
        { }

        public DbSet<Paper> Papers { get; set; }
    }
}
