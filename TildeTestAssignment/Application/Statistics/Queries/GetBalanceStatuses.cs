using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Common.Pagination;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBalanceStatuses : IRequestHandler<GetBalanceStatuses.Query, PaginatedResult<BalanceStatusVM>>
    {
        public class Query : PaginatedQuery, IRequest<PaginatedResult<BalanceStatusVM>>
        {
            public class Validator : PaginatedQueryValidatorTemplate<Query>
            { }
        }

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetBalanceStatuses(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<BalanceStatusVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            var persons = await _applicationDbContext.Persons
                .AsNoTracking()
                .ToPaginatedResultAsync(request, cancellationToken);

            return _mapper.Map<PaginatedResult<BalanceStatusVM>>(persons);
        }
    }
}