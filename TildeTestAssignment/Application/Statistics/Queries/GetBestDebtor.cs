using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;
using TildeTestAssignment.Web;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBestDebtor : IRequestHandler<GetBestDebtor.Query, BestDebtorVM>
    {
        public class Query : IRequest<BestDebtorVM>
        { }

        private readonly IApplicationDbContext _applicationDbContext;

        public GetBestDebtor(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BestDebtorVM> Handle(Query request, CancellationToken cancellationToken)
        {
            var bestDebtor = await _applicationDbContext.Persons
                .AsNoTracking()
                .Select(p => new BestDebtorVM
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    RefundedRelative = p.DebtorDebts.Any() ?
                        p.DebtorDebts.Sum(d => d.Refunded) / p.DebtorDebts.Sum(d => d.Amount) :
                        0,
                    TotalDebtAmount = p.DebtorDebts.Sum(d => d.Amount)
                })
                .OrderByDescending(x => x.RefundedRelative)
                .ThenByDescending(x => x.TotalDebtAmount)
                .FirstOrDefaultAsync(cancellationToken);

            if (bestDebtor == null)
            {
                throw new HttpStatusException("There are no persons", StatusCodes.Status404NotFound);
            }

            return bestDebtor;
        }
    }
}