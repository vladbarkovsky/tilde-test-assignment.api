using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Common.Pagination;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetAverageDebts : IRequestHandler<GetAverageDebts.Query, PaginatedResult<AverageDebtVM>>
    {
        public class Query : PaginatedQuery, IRequest<PaginatedResult<AverageDebtVM>>
        {
            public class Validator : PaginatedQueryValidatorTemplate<Query>
            { }
        }

        private readonly IApplicationDbContext _applicationDbContext;

        public GetAverageDebts(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<PaginatedResult<AverageDebtVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Persons
                .Select(p => new AverageDebtVM
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    AverageDebt = p.DebtorDebts.Where(d => !d.Paid).Average(d => d.LeftToPay)
                })
                .ToPaginatedResultAsync(request, cancellationToken);
        }
    }
}