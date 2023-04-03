
using Microsoft.EntityFrameworkCore;
using MVC.Models;

public class UserContext : DbContext
{
    public UserContext() 
    {
        Database.EnsureCreated();
    }

    public DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=userDb;Trusted_Connection=True;");
    }

}

