using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameAPI.Data.Entities;

namespace VideoGameAPI.Data.Repository
{
    public interface IVideogameRepository
    {
        //videogames 
        void CreateVideogame(VideoGameEntity videoGame);
        Task<VideoGameEntity> GetVideogameAsync(int videogameId);
        Task<IEnumerable<VideoGameEntity>> GetVideoGamesAsync();
        Task<bool> UpdateVideogameAsync(VideoGameEntity videoGame);
        bool DeleteVideogame(int videogameId);

        //save changes
        Task<bool> SaveChangesAsync();
    }
}
