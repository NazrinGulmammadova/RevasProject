using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Revas.Core.Entities;
using System.Collections.Generic;

namespace Revas.DataAccess.Contexts;

public class AppDbContext : IdentityDbContext<UserSide>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<Portfolio> Portfolios { get; set; } = null!;

}
