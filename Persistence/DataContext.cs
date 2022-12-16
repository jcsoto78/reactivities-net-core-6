using Domain;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{

    public class DataContext : DbContext
    {
        // DataContext is abstraction of the DB
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        // DbSet is abstraction of a DB table
        // Entity Activity is abstraction of a table Row
        // properties within an Entity are abstractions of db columns
        public DbSet<Activity> Activities { get; set; }

        

    }

}