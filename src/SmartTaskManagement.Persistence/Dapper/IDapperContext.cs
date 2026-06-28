using System.Data;

namespace SmartTaskManagement.Persistence.Dapper;

public interface IDapperContext
{
    IDbConnection CreateConnection();
}