//import statements
using Microsoft.EntityFrameworkCore;
using backEnd.Models;
using Microsoft.AspNetCore.Http.Features;

namespace backEnd.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
    
}