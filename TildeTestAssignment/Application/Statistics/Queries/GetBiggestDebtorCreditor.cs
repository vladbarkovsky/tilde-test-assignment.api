using MediatR;
using Microsoft.EntityFrameworkCore;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;
using TildeTestAssignment.Web;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBiggestDebtorCreditor : IRequestHandler<GetBiggestDebtorCreditor.Query, BiggestDebtorCreditorVM>
    {
        public class Query : IRequest<BiggestDebtorCreditorVM>
        { }

        private readonly IApplicationDbContext _applicationDbContext;

        public GetBiggestDebtorCreditor(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<BiggestDebtorCreditorVM> Handle(Query request, CancellationToken cancellationToken)
        {
            var biggestDebtorCreditor = await _applicationDbContext.Persons
                .Select(p => new BiggestDebtorCreditorVM
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    TotalDebt = p.DebtorDebts.Sum(d => d.Amount - d.Refunded),
                    TotalCredit = p.CreditorDebts.Sum(d => d.Amount - d.Refunded)
                })
                .OrderByDescending(x => x.TotalDebt)
                .ThenByDescending(x => x.TotalCredit)
                .FirstOrDefaultAsync(cancellationToken);

            if (biggestDebtorCreditor == null)
            {
                throw new HttpStatusException("There are no persons.", StatusCodes.Status404NotFound);
            }

            return biggestDebtorCreditor;
        }
    }
}