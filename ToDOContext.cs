using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToDO.Model;

namespace ToDO.Data
{
    public class ToDOContext : DbContext
    {
        public ToDOContext (DbContextOptions<ToDOContext> options)
            : base(options)
        {
        }

        public DbSet<ToDO.Model.toDo> toDo { get; set; } = default!;
    }
}
