using ELib.BL.Services.Abstract;
using System.Collections.Generic;
using ELib.DAL.Infrastructure.Abstract;
using ELib.BL.Mapper.Abstract;

namespace ELib.BL.Services.Concrete
{
    public class BaseService<TEntity, TEntityDto> : IBaseService<TEntity, TEntityDto> 
        where TEntityDto : class
        where TEntity : class
    {
        protected readonly IUnitOfWorkFactory _factory;
        protected readonly IMapper<TEntity,TEntityDto> _mapper;

        public BaseService(IUnitOfWorkFactory factory, IMapper<TEntity,TEntityDto> mapper)
        {
            _factory = factory;
            _mapper = mapper;
        }

        public int TotalCount { get; protected set; }
        

        public void Delete(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToDelete = _mapper.Map(entity);
                uow.Repository<TEntity>().Delete(entityToDelete);
                uow.Save();
            }
        }

        public void DeleteById(object id)
        {
            using (var uow = _factory.Create())
            {
                uow.Repository<TEntity>().DeleteById(id);
                uow.Save();
            }
        }

        public IEnumerable<TEntityDto> GetAll()
        {
            using (var uow = _factory.Create())
            {
                var entitiesDto = new List<TEntityDto>();
                var entities = uow.Repository<TEntity>().Get();
                TotalCount = uow.Repository<TEntity>().TotalCount;
                foreach(var item in entities)
                {
                    var entityDto = _mapper.Map(item);
                    entitiesDto.Add(entityDto);
                }

                return entitiesDto;
            }
        }

        public TEntityDto GetById(object id)
        {
            using (var uow = _factory.Create())
            {
                var entity = uow.Repository<TEntity>().GetById(id);
                var entityDto = _mapper.Map(entity);
                return entityDto;
            }
        }

        public void Insert(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToInsert = _mapper.Map(entity);
                uow.Repository<TEntity>().Insert(entityToInsert);
                uow.Save();
                entity = _mapper.Map(entityToInsert);
            }
        }

        public void Update(TEntityDto entity)
        {
            using (var uow = _factory.Create())
            {
                var entityToUpdate = _mapper.Map(entity);
                uow.Repository<TEntity>().Update(entityToUpdate);
                uow.Save();
            }
        }
    }
}
