using apiGames.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> GetPageList(int page, int quantity);
        Task<Game> GetById(Guid id);
        Task<List<Game>> Get(string name, string publisher);
        Task Insert(Game game);
        Task Update(Game game);
        Task Remove(Guid id);
    }
}
