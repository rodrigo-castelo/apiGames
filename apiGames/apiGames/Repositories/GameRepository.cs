using apiGames.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Repositories
{
    public class GameRepository : IGameRepository
    {
        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>()
        {
            {Guid.Parse("b54842b0-6323-4763-a960-c066ef15d1b0"), new Game{ Id = Guid.Parse("b54842b0-6323-4763-a960-c066ef15d1b0"), Name = "FIFA 21", Publisher = "EA", Genre = "Sports", ReleaseYear = 2020, Price = 200 }},
            {Guid.Parse("36ae77ca-38d1-44e2-bfd9-d704515f1a60"), new Game{ Id = Guid.Parse("36ae77ca-38d1-44e2-bfd9-d704515f1a60"), Name = "FIFA 20", Publisher = "EA", Genre = "Sports", ReleaseYear = 2019, Price = 180 }},
            {Guid.Parse("18696a7f-4eff-4532-bc2f-a2e464a33667"), new Game{ Id = Guid.Parse("18696a7f-4eff-4532-bc2f-a2e464a33667"), Name = "FIFA 19", Publisher = "EA", Genre = "Sports", ReleaseYear = 2018, Price = 150 }}
        };
        public void Dispose() { }

        public Task<List<Game>> Get(string name, string publisher)
        {
            var returnList = new List<Game>();
            foreach(var game in games.Values)
            {
                if (game.Name.Equals(name) && game.Publisher.Equals(publisher))
                    returnList.Add(game);
            }
            return Task.FromResult(returnList);
        }

        public Task<Game> GetById(Guid id)
        {
            if (!games.ContainsKey(id))
                return null;
            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> GetPageList(int page, int quantity)
        {
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task Insert(Game game)
        {
            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Remove(Guid id)
        {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public Task Update(Game game)
        {
            games[game.Id] = game;
            return Task.CompletedTask;
        }
    }
}
