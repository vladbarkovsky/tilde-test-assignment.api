using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBalanceStatuses : IRequestHandler<GetBalanceStatuses.Query, List<BalanceStatusVM>>
    {
        public class Query : IRequest<List<BalanceStatusVM>>
        { }

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        public GetBalanceStatuses(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
        }

        public async Task<List<BalanceStatusVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            var persons = await _applicationDbContext.Persons.ToListAsync(cancellationToken);
            return _mapper.Map<List<BalanceStatusVM>>(persons);
        }
    }
}