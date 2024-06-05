using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Entities;
using TildeTestAssignment.ORM.Services.Interfaces;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetTop : IRequestHandler<GetTop.Query, Unit>
    {
        public class Query : IRequest<Unit>
        { }

        private readonly IApplicationDbContext _applicationDbContext;

        public GetTop(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Unit> Handle(Query request, CancellationToken cancellationToken)
        {
            var maxRefundsCount = await _applicationDbContext.Persons
                .Include(x => x.CreditorTransactions.Where(x => x.Type == TransactionType.Loan))
                .MaxAsync(x => x.CreditorTransactions.Count, cancellationToken);

            var topByRefundsCount = _applicationDbContext.Persons
                .Include(x => x.CreditorTransactions.Where(x => x.Type == TransactionType.Loan))
                .Where(x => x.CreditorTransactions.Count == maxRefundsCount)
                .ToListAsync(cancellationToken);

            var maxRefundsSum = await _applicationDbContext.Persons
                .Include(x => x.CreditorTransactions.Where(x => x.Type == TransactionType.Loan))
                .MaxAsync(x => x.CreditorTransactions.Sum(x => x.Amount), cancellationToken);

            var topByRefundsSum = await _applicationDbContext.Persons
                .Include(x => x.CreditorTransactions.Where(x => x.Type == TransactionType.Loan))
                .Where(x => x.CreditorTransactions.Sum(x => x.Amount) == maxRefundsSum)
                .ToListAsync(cancellationToken);
        }

        private async Task<TopDataSetVM> GetTopDatasetAsync(Expression<Func<Person, List<Transaction>>> transactions)
        {
            var maxLoansCount = await _applicationDbContext.Persons
    .Include(x => x.DebtorTransactions.Where(x => x.Type == TransactionType.Loan))
    .MaxAsync(x => x.DebtorTransactions.Count, cancellationToken);

            var topByLoansCount = _applicationDbContext.Persons
                .Include(x => x.DebtorTransactions.Where(x => x.Type == TransactionType.Loan))
                .Where(x => x.DebtorTransactions.Count == maxLoansCount)
                .ToListAsync(cancellationToken);

            var maxLoansSum = await _applicationDbContext.Persons
                .Include(x => x.DebtorTransactions.Where(x => x.Type == TransactionType.Loan))
                .MaxAsync(x => x.DebtorTransactions.Sum(x => x.Amount), cancellationToken);

            var topByLoansSum = await _applicationDbContext.Persons
                .Include(x => x.DebtorTransactions.Where(x => x.Type == TransactionType.Loan))
                .Where(x => x.DebtorTransactions.Sum(x => x.Amount) == maxLoansSum)
                .ToListAsync(cancellationToken);
        }
    }
}