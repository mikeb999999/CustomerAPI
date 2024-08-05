using AutoMapper;
using CustomerAPI.Entities;

namespace CustomerAPI
{
    public class AutoMapperProfile : Profile
    {
        #region Constructors

        public AutoMapperProfile()
        {
            //Create mapping for the entities and models
            CreateMappings<Customer, Models.Customer>();
 
        }

        #endregion

        /// <summary> Generate a mapping between the entity types </summary>
        private void CreateMappings<TEntity, TModel>()
        {
            //Create the model to entity AND entity to model mapping
            CreateMap<TModel, TEntity>().ReverseMap();

            //Create the model to model mapping
            CreateMap<TModel, TModel>();

            //Create the entity to entity mapping
            CreateMap<TEntity, TEntity>();
        }

    }
}
