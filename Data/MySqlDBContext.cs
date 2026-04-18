using Microsoft.EntityFrameworkCore;
using MiProyectoMVC.Models.Events;

namespace MiProyectoMVC.Data;

public class MySqlDBContext : DbContext
{
    public MySqlDBContext(DbContextOptions<MySqlDBContext> options) : base(options)
    {}
    
    public DbSet<Event> Events { get; set; }
}