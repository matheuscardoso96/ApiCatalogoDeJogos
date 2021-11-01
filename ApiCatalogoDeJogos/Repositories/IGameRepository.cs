using ApiCatalogoDeJogos.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiCatalogoDeJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Get(int page, int count);
        Task<List<Game>> Get(string name, string developer);
        Task<Game> Get(Guid id);
        Task Insert(Game game);
        Task Update(Game game);
        Task Delete(Guid id);
    }
}
