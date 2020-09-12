using Dapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.ConnectionStrings;
using DogKeepers.Shared.QueryFilters;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly string connectionString;

        public UserRepository(IOptions<ConnectionString> connectionString)
        {
            this.connectionString = connectionString.Value.Production;
        }

        public async Task<User> Get(int id)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<User>($"select * from users where id = {id}");

                user = sqlResponse.FirstOrDefault();
            }

            return user;
        }

        public async Task<User> GetAuth(SignInQueryFilter model)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<User>($"select * from users where email = '{model.Email}' and password = '{model.Password}'");

                user = sqlResponse.FirstOrDefault();
            }

            return user;
        }

        public async Task<bool> GetByEmailPhone(string email, string phone)
        {
            User user = null;
            string condition = "";

            condition = string.IsNullOrEmpty(email) ? "" : $" and email = '{email}'";
            condition += string.IsNullOrEmpty(phone) ? "" : $" and phone = '{phone}'";

            condition = condition != "" ? $"where {condition.Substring(4)}" : "";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<User>($"select * from users {condition}");

                user = sqlResponse.FirstOrDefault();
            }

            return (user != null);
        }

        public async Task<User> Post(SignUpQueryFilter model)
        {
            User user = null;

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await connection.QueryAsync<int>($@"
                        insert into
                            users
                                (name, email, password, dateBirth, phone, adoptionRating, registrationDate)
                            values
                                ('{model.Name}', '{model.Email.ToLower()}', '{model.Password}', '{Convert.ToDateTime(model.Birthday).ToString("yyyy-MM-dd")}', '{model.Phone.ToLower()}', '10', now());

                        SELECT CAST(LAST_INSERT_ID() AS UNSIGNED INTEGER);
                    ");

                user = new User() { Id = sqlResponse.Single() };
            }

            return user;
        }




    }
}
