using apiGames.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apiGames.Services
{
    public interface IGameService :IDisposable
    {
        Task<List<GameViewModel>> GetPageList(int page, int quantity);
        Task<GameViewModel> GetById(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task PriceUpdate(Guid id, double price);
        Task Remove(Guid id);
    }
}
