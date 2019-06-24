using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kazidocs2.Models
{
    public class KazidocsContext : IdentityDbContext
    {
        public KazidocsContext(DbContextOptions<KazidocsContext> options) : base(options)
        {

        }

        public DbSet<UserAccounts> UserAccounts { get; set; }
        public DbSet<FormTypes> FormTypes { get; set; }
        public DbSet<FormNames> FormNames { get; set; }
        public DbSet<DataTypes> DataTypes { get; set; }
        public DbSet<FormFields> FormFields { get; set; }
        public DbSet<FormValues> FormValues { get; set; }
        public DbSet<UserTokens> Tokens { get;set; }
        public DbSet<Logins> Logins { get; set; }
    }
}
