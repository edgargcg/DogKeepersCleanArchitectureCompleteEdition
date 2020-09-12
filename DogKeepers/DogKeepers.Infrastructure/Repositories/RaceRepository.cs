using Dapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.ConnectionStrings;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Infrastructure.Repositories
{
    public class RaceRepository : IRaceRepository
    {

        private string connectionString;

        public RaceRepository(IOptions<ConnectionString> connectionString)
        {
            this.connectionString = connectionString.Value.Production;
        }

        public async Task<List<Race>> GetList()
        {
            List<Race> races = null;
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<Race>("select * from races");

                races = sqlResponse.ToList();
            }

            return races;
        }

    }
}
