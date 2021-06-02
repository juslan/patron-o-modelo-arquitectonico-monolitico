using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameAPI.Data.Entities;

namespace VideoGameAPI.Data.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        private LibraryDbContext _dbContext;

        public LibraryRepository(LibraryDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        public void CreateVideogame(VideoGameEntity videoGame)
        {
            _dbContext.Videogames.Add(videoGame);
        }

        public async Task<VideoGameEntity> GetVideogameAsync(int videogameId)
        {
            IQueryable<VideoGameEntity> query = _dbContext.Videogames;
            var videogame = await query.SingleOrDefaultAsync(v => v.Id == videogameId);
            return videogame;
        }

        public async Task<IEnumerable<VideoGameEntity>> GetVideoGamesAsync()
        {
            IQueryable<VideoGameEntity> query = _dbContext.Videogames;
            query = query.AsNoTracking();

            return await query.ToArrayAsync(); ;
        }

        public async Task<bool> UpdateVideogameAsync(VideoGameEntity videoGame)
        {
            var videogameToUpdate = await _dbContext.Videogames.FirstOrDefaultAsync(v => v.Id == videoGame.Id);
            videogameToUpdate.Name = videoGame.Name ?? videogameToUpdate.Name;
            videogameToUpdate.Price = videoGame.Price?? videogameToUpdate.Price;
            videogameToUpdate.Genre = videoGame.Genre ?? videogameToUpdate.Genre;
            videogameToUpdate.ESRB = videoGame.ESRB ?? videogameToUpdate.ESRB;
            return true;
        }

        public bool DeleteVideogame(int videogameId)
        {
            var videogameToDelete = new VideoGameEntity() { Id = videogameId };
            _dbContext.Entry(videogameToDelete).State = EntityState.Deleted;
            return true;
        }

        public async Task<bool> SaveChangesAsync()
        {
            try
            {
                var res = await _dbContext.SaveChangesAsync();
                return res > 0;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }
    }
}
