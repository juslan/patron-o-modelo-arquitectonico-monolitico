using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VideoGameAPI.Data.Entities;
using VideoGameAPI.Data.Repository;
using VideoGameAPI.Exceptions;
using VideoGameAPI.Models;

namespace VideoGameAPI.Services
{
    public class VidegamesService : IVidegamesService
    {
        private IMapper _mapper;
        private ILibraryRepository _libraryRepository;

        public VidegamesService(IMapper mapper, ILibraryRepository libraryRepository)
        {
            _mapper = mapper;
            _libraryRepository = libraryRepository;
        }

        public async Task<VideogameModel> CreateVideogameAsync(VideogameModel videogame)
        {
            var vidogameEntity = _mapper.Map<VideoGameEntity>(videogame);
            _libraryRepository.CreateVideogame(vidogameEntity);
            var saveResult = await _libraryRepository.SaveChangesAsync();
            if (!saveResult)
            {
                throw new Exception("save error");
            }

            var modelToReturn = _mapper.Map< VideogameModel>(vidogameEntity);
            return modelToReturn;
        }

        public async Task<bool> DeleteVideogameAsync(int videogameId)
        {
            await GetVideogameAsync(videogameId);
            _libraryRepository.DeleteVideogame(videogameId);
            var saveRestul = await _libraryRepository.SaveChangesAsync();
            if (!saveRestul)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }

        public async Task<VideogameModel> GetVideogameAsync(int videogameId)
        {
            await validateVideogame(videogameId);
            var videogame = await _libraryRepository.GetVideogameAsync(videogameId);
            return _mapper.Map<VideogameModel>(videogame);
        }

        public async Task<IEnumerable<VideogameModel>> GetAllVideogamesAsync()
        {
            var videogames = await _libraryRepository.GetVideoGamesAsync();
            return _mapper.Map<IEnumerable<VideogameModel>>(videogames);
        }

        public async Task<bool> UpdateVideogameAsync(int videogameId, VideogameModel videogame)
        {
            await GetVideogameAsync(videogameId);
            videogame.Id = videogameId;
            await _libraryRepository.UpdateVideogameAsync(_mapper.Map<VideoGameEntity>(videogame));
            var saveRestul = await _libraryRepository.SaveChangesAsync();
            if (!saveRestul)
            {
                throw new Exception("Error while saving.");
            }
            return true;
        }
        private async Task validateVideogame(int videogameId)
        {
            var videogame = await _libraryRepository.GetVideogameAsync(videogameId);
            if (videogame == null)
            {
                throw new NotFoundOperationException($"the videogame id:{videogameId}, does not exist");
            }
        }
    }
}
