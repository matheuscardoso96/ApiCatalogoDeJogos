using ApiCatalogoDeJogos.Models.InputModels;
using ApiCatalogoDeJogos.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Get(int page, int count);
        Task<GameViewModel> Get(Guid id);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid id, GameInputModel game);
        Task Update(Guid id, double price);
        Task Delete(Guid id);
    }
}
