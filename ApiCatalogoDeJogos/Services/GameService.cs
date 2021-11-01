using ApiCatalogoDeJogos.Exceptions;
using ApiCatalogoDeJogos.Models.Entities;
using ApiCatalogoDeJogos.Models.InputModels;
using ApiCatalogoDeJogos.Models.ViewModels;
using ApiCatalogoDeJogos.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Services
{
    public class GameService : IGameService, IDisposable
    {
        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
       
        public async Task<List<GameViewModel>> Get(int page, int count)
        {
            var games = await _gameRepository.Get(page,count);

            return games.Select(g => new GameViewModel { Id = g.Id, Name = g.Name,
                Developer = g.Developer, Price = g.Price }).ToList();
        }

        public async Task<GameViewModel> Get(Guid id)
        {
            var game = await _gameRepository.Get(id);

            if (game == null)
            {
                return null;
            }

            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Developer = game.Developer,
                Price = game.Price
            };
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var gameE = await _gameRepository.Get(game.Name, game.Developer);
            if (gameE.Count > 0)
            {
                throw new GameAlreadyInDataBaseException();
            }

            var gameToInsert = new Game(Guid.NewGuid(), game.Name, game.Developer, game.Price);

            await _gameRepository.Insert(gameToInsert);

            return new GameViewModel
            {
                Id = gameToInsert.Id,
                Name = gameToInsert.Name,
                Developer = gameToInsert.Developer,
                Price = gameToInsert.Price
            };
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var gameE = await _gameRepository.Get(id);
            if (gameE.Name == null)
            {
                throw new GameNotRegisteredException();
            }
            var gameToUpdate = new Game(id, game.Name, game.Developer, game.Price);

            await _gameRepository.Update(gameToUpdate);

        }

        public async Task Update(Guid id, double price)
        {
            var gameE = await _gameRepository.Get(id);
            if (gameE.Name == null)
            {
                throw new GameNotRegisteredException();
            }
            gameE.Price = price;
            
            await _gameRepository.Update(gameE);
        }

        public async Task Delete(Guid id)
        {
            var gameE = await _gameRepository.Get(id);
            if (gameE.Name == null)
            {
                throw new GameNotRegisteredException();
            }

            await _gameRepository.Delete(id);
        }

        public void Dispose()
        {
            _gameRepository?.Dispose();
        }
    }
}
