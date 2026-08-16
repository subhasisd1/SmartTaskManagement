using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace SmartTaskManagement.Persistence.Contexts;

public class MySqlDbContextFactory
    : IDesignTimeDbContextFactory<MySqlDbContext>
{
    public MySqlDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<MySqlDbContext>();

        var connectionString =
            "Server=localhost;" +
            "Port=3306;" +
            "Database=smarttaskmanagement;" +
            "User=root;" +
            "Password=root;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString));

        return new MySqlDbContext(
            optionsBuilder.Options);
    }
}