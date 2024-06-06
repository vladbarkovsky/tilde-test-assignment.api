using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using TildeTestAssignment.Application.Common;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;
using TildeTestAssignment.Web;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetAverageDebts : IRequestHandler<GetAverageDebts.Query, List<AverageDebtVM>>
    {
        public class Query : IRequest<List<AverageDebtVM>>, ISortQuery, ISearchQuery
        {
            public string? SortBy { get; set; }
            public SortDirection? SortDirection { get; set; }
            public string? SearchText { get; set; }
        }

        private readonly IApplicationDbContext _applicationDbContext;

        private FrozenDictionary<string, Func<AverageDebtVM, object>> SortMap { get; }

        public GetAverageDebts(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

            SortMap = new Dictionary<string, Func<AverageDebtVM, object>>
            {
                { "firstName", x => x.FirstName },
                { "lastName", x => x.LastName },
                { "debt", x => x.Debt }
            }.ToFrozenDictionary();
        }

        public async Task<List<AverageDebtVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            var averageDebts = await _applicationDbContext.Persons
                .AsNoTracking()
                .Select(p => new AverageDebtVM
                {
                    PersonId = p.Id,
                    FirstName = p.FirstName,
                    LastName = p.LastName,
                    Debt = Math.Round(
                        p.DebtorDebts
                            .Where(d => d.Amount > d.Refunded)
                            .Average(d => d.Amount - d.Refunded),
                        2,
                        MidpointRounding.AwayFromZero)
                })
                .ToListAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                averageDebts = averageDebts
                    .Where(x =>
                        x.FirstName.ToLower().Contains(request.SearchText.ToLower()) ||
                        x.LastName.ToLower().Contains(request.SearchText.ToLower()) ||
                        x.Debt.ToString("0.00").Contains(request.SearchText.ToLower()))
                    .ToList();
            }

            if (!string.IsNullOrEmpty(request.SortBy))
            {
                var sortExpressionFound = SortMap.TryGetValue(request.SortBy, out var sortExpression);

                if (!sortExpressionFound)
                {
                    throw new HttpStatusException("Invalid sort parameter sortBy.");
                }

                // TODO: Ordering triggers query to database. Investigate.
                var ordered = (request.SortDirection ?? SortDirection.Ascending) == SortDirection.Ascending ?
                   averageDebts.OrderBy(sortExpression) :
                   averageDebts.OrderByDescending(sortExpression);

                return [.. ordered];
            }

            return averageDebts;
        }
    }
}