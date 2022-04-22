using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using peliculaAPI.Models;

namespace peliculaAPI.Data
{
    public class peliculaAPIContext : DbContext
    {
        public peliculaAPIContext (DbContextOptions<peliculaAPIContext> options)
            : base(options)
        {
        }

        public DbSet<peliculaAPI.Models.Pelicula> Pelicula { get; set; }
    }
}
