using AutoMapper;

namespace TildeTestAssignment.Application.Common.Pagination
{
    public class PaginatedResultMappingProfile : Profile
    {
        public PaginatedResultMappingProfile()
        {
            CreateMap(typeof(PaginatedResult<>), typeof(PaginatedResult<>));
        }
    }
}