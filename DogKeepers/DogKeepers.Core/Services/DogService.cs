using DogKeepers.Core.Entities;
using DogKeepers.Core.Exceptions;
using DogKeepers.Core.Interfaces.Repositories;
using DogKeepers.Core.Interfaces.Services;
using DogKeepers.Core.Interfaces.Utils;
using DogKeepers.Core.Options;
using DogKeepers.Core.Response;
using DogKeepers.Core.Utils;
using DogKeepers.Shared.QueryFilters;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DogKeepers.Core.Services
{
    public class DogService : IDogService
    {

        private readonly IFileUtil fileUtil;
        private readonly PaginationOption pagination;
        private readonly IDogRepository dogRepository;
        private readonly IOptions<PathOptions> pathOptions;

        public DogService(
            IFileUtil fileUtil,
            IDogRepository dogRepository, 
            IOptions<PathOptions> pathOptions,
            IOptions<PaginationOption> paginationOption
        )
        {
            this.fileUtil = fileUtil;
            this.pathOptions = pathOptions;
            this.dogRepository = dogRepository;
            this.pagination = paginationOption.Value;
        }

        public async Task<Response<Dog>> Get(long id)
        {
            var dog = await dogRepository.Get(id);

            if (dog == null)
                throw new BusinessException("El perrito ya fue adoptado o fue dado de baja del sistema");

            dog.Picture =
                (dog.Picture == null)
                    ? new DogPicture() { Picture = pathOptions.Value.DefaultDogPicture }
                    : new DogPicture() { Picture = pathOptions.Value.DogsPicturesPath + dog.Picture.Picture };

            dog.Picture.PictureFile = fileUtil.GetFile(dog.Picture.Picture);

            return new Response<Dog>(true, "", dog);
        }

        public async Task<Response<PagedList<Dog>>> GetList(DogListQueryFilter model)
        {
            model.PageNumber = pagination.DefaultPageNumber;
            model.PageSize = pagination.DefaultPageSize;

            var dogsInfo = await dogRepository.GetList(model);
            var serviceResponse = new Response<PagedList<Dog>>(
                true, 
                "", 
                PagedList<Dog>.Create(dogsInfo.Item2, (int)model.PageNumber, (int)model.PageSize, dogsInfo.Item1)
            );

            return serviceResponse;
        }




    }
}
