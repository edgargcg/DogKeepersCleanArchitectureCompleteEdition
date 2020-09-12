using Dapper;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.ConnectionStrings;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Infrastructure.Repositories
{
    public class BaseRepository : IBaseRepository
    {

        private string connectionString;

        public BaseRepository(IOptions<ConnectionString> connectionString)
        {
            this.connectionString = connectionString.Value.Production;
        }

        public async Task<int> Count(string command)
        {
            int count = 0;

            using (var connection = new MySqlConnection(connectionString))
            {
                var sqlResponse = await connection.QueryAsync<int>(command);
                count = sqlResponse.First();
            }

            return count;
        }
    }
}
