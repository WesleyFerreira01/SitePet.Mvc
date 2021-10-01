using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SitePet.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SitePet.Mvc.ViewModels;

namespace SitePet.Mvc.Data
{
    public class MeuDbContext : IdentityDbContext
    {
        public MeuDbContext(DbContextOptions<MeuDbContext> options) : base (options)
        {

        }

        public DbSet<Pet> Pets { get; set; }

     
    }
}
