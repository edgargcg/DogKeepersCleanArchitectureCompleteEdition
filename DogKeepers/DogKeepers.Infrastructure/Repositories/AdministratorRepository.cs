using Dapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.ConnectionStrings;
using DogKeepers.Shared.QueryFilters;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Infrastructure.Repositories
{
    public class AdministratorRepository : IAdministratorRepository
    {

        private readonly string connectionString;

        public AdministratorRepository(IOptions<ConnectionString> connectionString)
        {
            this.connectionString = connectionString.Value.Production;
        }

        public async Task<Administrator> GetAuth(SignInQueryFilter model)
        {
            Administrator administrator = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<Administrator>($"select * from administrator where email = '{model.Email}' and password = '{model.Password}' and active = 1");

                administrator = sqlResponse.FirstOrDefault();
            }

            return administrator;
        }
    }
}
