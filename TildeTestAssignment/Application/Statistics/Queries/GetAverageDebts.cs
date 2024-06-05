using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetAverageDebts : IRequestHandler<GetAverageDebts.Query, List<AverageDebtVM>>
    {
        public class Query : IRequest<List<AverageDebtVM>>
        { }

        private readonly IApplicationDbContext _applicationDbContext;

        public GetAverageDebts(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<AverageDebtVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Persons
                .Select(p => new AverageDebtVM
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    AverageDebt = Math.Round(
                        p.DebtorDebts
                            .Where(d => d.Amount > d.Refunded)
                            .Average(d => d.Amount - d.Refunded),
                        2,
                        MidpointRounding.AwayFromZero)
                })
                .ToListAsync(cancellationToken);
        }
    }
}