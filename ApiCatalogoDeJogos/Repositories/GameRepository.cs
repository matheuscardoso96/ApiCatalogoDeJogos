using ApiCatalogoDeJogos.Models.Entities;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly SqlConnection _sqlConnection;
        public GameRepository(IConfiguration configuration)
        {
            _sqlConnection = new SqlConnection(configuration.GetConnectionString("Default"));
        }

        public async Task<List<Game>> Get(int page, int count)
        {
            var games = new List<Game>();
            var command = $"Select * from Game order by id offset {(page - 1) * count} rows fetch next {count} rows only;";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command,_sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                games.Add(new Game(
                    (Guid)sqlDataReader["Id"],
                    (string)sqlDataReader["name"],
                    (string)sqlDataReader["developer"],
                    (double)sqlDataReader["price"]));
            }
            await _sqlConnection.CloseAsync();
            return games;
        }

        public async Task<List<Game>> Get(string name, string developer)
        {
            var games = new List<Game>();
            var command = $"Select * from Game where name = '{name}' and developer = '{developer}';";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                games.Add(new Game(
                    (Guid)sqlDataReader["Id"],
                    (string)sqlDataReader["name"],
                    (string)sqlDataReader["developer"],
                    (double)sqlDataReader["price"]));
            }
            await _sqlConnection.CloseAsync();
            return games;
        }

        public async Task<Game> Get(Guid id)
        {
            var game = new Game();
            var command = $"Select * from Game where Id = '{id}'";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            var sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            while (sqlDataReader.Read())
            {
                game = new Game(
                    (Guid)sqlDataReader["Id"],
                    (string)sqlDataReader["name"],
                    (string)sqlDataReader["developer"],
                    (double)sqlDataReader["price"]);
            }
            await _sqlConnection.CloseAsync();
            return game;
        }

        public async Task Insert(Game game)
        {
            var command = $"Insert into Game(id, name, developer, price) Values('{game.Id}', '{game.Name}', '{game.Developer}', '{game.Price}');";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            _ = await sqlCommand.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
        }

        public async Task Update(Game game)
        {
            var command = $"Update Game set name = '{game.Name}', developer = '{game.Developer}', price = '{game.Price}' where id = '{game.Id}';";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            _ = await sqlCommand.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
        }

        public async Task Delete(Guid id)
        {
            var command = $"Delete from Game where id = '{id}';";
            await _sqlConnection.OpenAsync();
            var sqlCommand = new SqlCommand(command, _sqlConnection);
            _ = await sqlCommand.ExecuteNonQueryAsync();
            await _sqlConnection.CloseAsync();
        }

        public void Dispose()
        {
            _sqlConnection?.Dispose();
            _sqlConnection?.Close();
        }
    }
}
