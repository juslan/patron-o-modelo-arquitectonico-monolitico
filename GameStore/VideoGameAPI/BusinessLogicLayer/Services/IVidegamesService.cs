using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameAPI.Models;

namespace VideoGameAPI.Services
{
    public interface IVidegamesService
    {
        Task<VideogameModel> CreateVideogameAsync( VideogameModel videogame);
        Task<VideogameModel> GetVideogameAsync(int videogameId);
        Task<IEnumerable<VideogameModel>> GetAllVideogamesAsync();
        Task<bool> UpdateVideogameAsync(int videogameId, VideogameModel videogame);
        Task<bool> DeleteVideogameAsync(int videogameId);
    }
}
