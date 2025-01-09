namespace ModularMonolith.Modules.Ordering
{
    using AutoMapper;
    using ModularMonolith.Modules.Articles.Api;
    using ModularMonolith.Modules.Contractors.Api.Dtos;
    using ModularMonolith.Modules.Ordering.Domain;
    using ModularMonolith.Modules.Warehouses.Api;

    internal class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ArticleDto, Article>()
                .ForCtorParam(nameof(Article.Id), opt => opt.MapFrom(src => new ArticleId(src.TypeId, src.Id)))
                .ForCtorParam(nameof(Article.Code), opt => opt.MapFrom(src => src.Code))
                .ForCtorParam(nameof(Article.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(Article.Unit), opt => opt.MapFrom(src => src.Unit))
                .ForCtorParam(nameof(Article.IsBlocked), opt => opt.MapFrom(src => src.IsBlocked));

            CreateMap<WarehouseDto, Warehouse>()
                .ForCtorParam(nameof(Warehouse.Id), opt => opt.MapFrom(src => new WarehouseId(src.TypeId, src.Id)))
                .ForCtorParam(nameof(Warehouse.Code), opt => opt.MapFrom(src => src.Code))
                .ForCtorParam(nameof(Warehouse.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(Warehouse.IsBlocked), opt => opt.MapFrom(src => src.IsBlocked));

            CreateMap<ContractorDto, Contractor>()
                .ForCtorParam(nameof(Contractor.Id), opt => opt.MapFrom(src => new ContractorId(src.TypeId, src.Id)))
                .ForCtorParam(nameof(Contractor.Code), opt => opt.MapFrom(src => src.Code))
                .ForCtorParam(nameof(Contractor.Name), opt => opt.MapFrom(src => src.Name))
                .ForCtorParam(nameof(Contractor.IsBlocked), opt => opt.MapFrom(src => src.IsBlocked));
        }
    }
}
