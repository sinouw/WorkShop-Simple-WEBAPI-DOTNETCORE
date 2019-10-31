using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapitest.Models
{
    public class PersonneContext : DbContext
    {
        public PersonneContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Personne> Personnes { get; set; }

    }
}
