using apiGames.Entities;
using apiGames.Middlewares.Exceptions;
using apiGames.Models;
using apiGames.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        
        public void Dispose()
        {
            _gameRepository?.Dispose();
        }

        public async Task<GameViewModel> GetById(Guid id)
        {
            var game = await _gameRepository.GetById(id);
            if (game == null)
                return null;
            return new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Price = game.Price
            };
        }

        
        public async Task<List<GameViewModel>> GetPageList(int page, int quantity)
        {
            var games = await _gameRepository.GetPageList(page, quantity);
            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Price = game.Price
            }).ToList();
        }

        public async Task<GameViewModel> Insert(GameInputModel game)
        {
            var entityGame = await _gameRepository.Get(game.Name, game.Publisher);

            if (entityGame.Count > 0)
                throw new AlreadyRegisteredException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Name = game.Name,
                Publisher = game.Publisher,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Price = game.Price
            };

            await _gameRepository.Insert(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Name = game.Name,
                Publisher = game.Publisher,
                Genre = game.Genre,
                ReleaseYear = game.ReleaseYear,
                Price = game.Price
            };
        }

        public async Task PriceUpdate(Guid id, double price)
        {
            var entityGame = await _gameRepository.GetById(id);

            if (entityGame == null)
                throw new NonRegisteredException();

            entityGame.Price = price;
            await _gameRepository.Update(entityGame);
        }

        public async Task Remove(Guid id)
        {
            var game = await _gameRepository.GetById(id);
            if (game == null)
                throw new NonRegisteredException();
            await _gameRepository.Remove(id);
        }

        public async Task Update(Guid id, GameInputModel game)
        {
            var entityGame = await _gameRepository.GetById(id);

            if (entityGame == null)
                throw new NonRegisteredException();

            entityGame.Name = game.Name;
            entityGame.Publisher = game.Publisher;
            entityGame.Genre = game.Genre;
            entityGame.ReleaseYear = game.ReleaseYear;
            entityGame.Price = game.Price;

            await _gameRepository.Update(entityGame);
        }
    }
}
