using System.Data;

namespace firstProject.Models
{
    public interface IDbConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}