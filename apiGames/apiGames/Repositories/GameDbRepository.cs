using apiGames.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Repositories
{
    public class GameDbRepository : IGameRepository
    {
        private readonly SqlConnection sqlConnection;

        public GameDbRepository(IConfiguration configuration)
        {
            sqlConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }

        public void Dispose()
        {
            sqlConnection?.Close();
            sqlConnection?.Dispose();
        }

        public async Task<List<Game>> Get(string name, string publisher)
        {
            var games = new List<Game>();
            var command = $"select * from TB_GAMES where Name = '{name}' and Publisher = '{publisher}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Publisher = (string)sqlDataReader["Publisher"],
                    Genre = (string)sqlDataReader["Genre"],
                    ReleaseYear = (int)sqlDataReader["ReleaseYear"],
                    Price = (double)sqlDataReader["Price"]
                });
            }
            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> GetById(Guid id)
        {
            Game game = null;
            var command = $"select * from TB_GAMES where Id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                game = new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Publisher = (string)sqlDataReader["Publisher"],
                    Genre = (string)sqlDataReader["Genre"],
                    ReleaseYear = (int)sqlDataReader["ReleaseYear"],
                    Price = (double)sqlDataReader["Price"]
                };
            }
            await sqlConnection.CloseAsync();
            return game;
        }

        public async Task<List<Game>> GetPageList(int page, int quantity)
        {
            var games = new List<Game>();
            var command = $"select * from TB_GAMES order by id offset {((page - 1) * quantity)} rows fetch next {quantity} rows only";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

            while (sqlDataReader.Read())
            {
                games.Add(new Game
                {
                    Id = (Guid)sqlDataReader["Id"],
                    Name = (string)sqlDataReader["Name"],
                    Publisher = (string)sqlDataReader["Publisher"],
                    Genre = (string)sqlDataReader["Genre"],
                    ReleaseYear = (int)sqlDataReader["ReleaseYear"],
                    Price = (double)sqlDataReader["Price"]
                });
            }
            await sqlConnection.CloseAsync();
            return games;
        }

        public async Task Insert(Game game)
        {
            var command = $"insert TB_GAMES (Id, Name, Publisher, Genre, ReleaseYear, Price) values ('{game.Id}','{game.Name}','{game.Publisher}','{game.Genre}','{game.ReleaseYear}','{game.Price.ToString().Replace(",",".")}')";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Remove(Guid id)
        {
            var command = $"delete from TB_GAMES where id = '{id}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"update TB_GAMES set Name = '{game.Name}', Publisher = '{game.Publisher}', Genre = '{game.Genre}', ReleaseYear = '{game.ReleaseYear}', Price = '{game.Price}'";
            await sqlConnection.OpenAsync();
            SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
            sqlCommand.ExecuteNonQuery();
            await sqlConnection.CloseAsync();
        }
    }
}
