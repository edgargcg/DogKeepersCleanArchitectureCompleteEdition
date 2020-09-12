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
    public class SizeRepository : ISizeRepository
    {

        private readonly string connectionString;

        public SizeRepository(IOptions<ConnectionString> connectionString)
        {
            this.connectionString = connectionString.Value.Production;
        }

        public async Task<List<Size>> GetList()
        {
            List<Size> sizes = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<Size>("select * from sizes");

                sizes = sqlResponse.ToList();
            }

            return sizes;
        }
    }
}
