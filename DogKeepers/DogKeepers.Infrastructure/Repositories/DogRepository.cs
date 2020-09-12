using AutoMapper;
using Dapper;
using DogKeepers.Core.Entities;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Infrastructure.ConnectionStrings;
using DogKeepers.Shared.QueryFilters;
using Microsoft.Extensions.Options;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogKeepers.Infrastructure.Repositories
{
    public class DogRepository : IDogRepository
    {

        private string connectionString { get; set; }
        private IBaseRepository baseRepository { get; set; }

        public DogRepository(IOptions<ConnectionString> connectionString, IBaseRepository baseRepository)
        {
            this.connectionString = connectionString.Value.Production;
            this.baseRepository = baseRepository;
        }

        public async Task<Tuple<int, List<Dog>>> GetList(DogListQueryFilter model)
        {
            List<Dog> dogs = null;
            string order = "";
            string limit = "";
            string condition = "";
            string fromCommand = "";

            if (model.Random > 0)
            {
                order = "order by rand()";
                limit = $"limit {model.Random}";
            }
            else
            {
                condition += !string.IsNullOrEmpty(model.Name) ? $" and dogs.name like '%{model.Name.Replace(' ', '%')}%'" : "";
                condition += model.RaceId != 0 ? $" and raceId = {model.RaceId}" : "";
                condition += model.SizeId != 0 ? $" and sizeId = {model.SizeId}" : "";
                condition = condition != "" ? $"where {condition.Substring(4)}" : "";

                order = "order by dogs.id";

                var skipUntilItem = (model.PageNumber - 1) * model.PageSize;
                limit = $"limit {skipUntilItem}, {model.PageSize}";
            }

            fromCommand = $@"
                FROM
                    dogs
                    inner join races
                        on races.id = raceId
                    inner join sizes
                        on sizes.id = sizeId
            ";

            var sqlCommand =
                $@"
                    SELECT 
                        *
                    {fromCommand}
                    {condition}
                    {order} 
                    {limit}
                ";

            var sqlCountCommnad =
                $@"
                    SELECT 
                        count(*)
                    {fromCommand}
                    {condition}
                ";

            using (var connection = new MySqlConnection(connectionString))
            {
                var sqlResponse = await connection.QueryAsync<Dog, Race, Size, Dog>(
                    sqlCommand,
                    (d, r, s) =>
                    {
                        d.Race = r;
                        d.Size = s;

                        return d;
                    },
                    splitOn: "id, id" // first id = races.id second id = sizes.id
                );
                dogs = sqlResponse.ToList();
            }

            var count = await baseRepository.Count(sqlCountCommnad);

            return new Tuple<int, List<Dog>>(count, dogs);
        }

        public async Task<Dog> Get(long id)
        {
            Dog dog = null;

            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse = await connection.QueryAsync<Dog, Race, Size, Dog>(
                    $@"
                        select
                            *
                         FROM
                            dogs
                            inner join races
                                on races.id = raceId
                            inner join sizes
                                on sizes.id = sizeId
                        where   
                            dogs.id = {id}
                    ",
                    (d, r, s) =>
                    {
                        d.Race = r;
                        d.Size = s;

                        return d;
                    },
                    splitOn: "id, id"
                );

                dog = sqlResponse.FirstOrDefault();
            }

            if (dog != null)
                dog.Picture = await GetPicture(dog.Id);

            return dog;
        }

        public async Task<DogPicture> GetPicture(long id)
        {
            DogPicture dogPicture = null;

            using(MySqlConnection connection = new MySqlConnection(connectionString))
            {
                var sqlResponse =
                    await
                    connection.QueryAsync<DogPicture>($@"
                        SELECT * FROM dogpictures where dogId = {id}
                    ");

                dogPicture = sqlResponse.FirstOrDefault();
            }

            return dogPicture;
        }

    }
}
