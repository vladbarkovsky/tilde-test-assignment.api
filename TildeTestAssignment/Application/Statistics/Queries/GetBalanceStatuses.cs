using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Frozen;
using System.Linq;
using TildeTestAssignment.Application.Common;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.ORM.Services.Interfaces;
using TildeTestAssignment.Web;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBalanceStatuses : IRequestHandler<GetBalanceStatuses.Query, List<BalanceStatusVM>>
    {
        public class Query : IRequest<List<BalanceStatusVM>>, ISortQuery, ISearchQuery
        {
            public string? SortBy { get; set; }
            public SortDirection? SortDirection { get; set; }
            public string? SearchText { get; set; }
        }

        private readonly IApplicationDbContext _applicationDbContext;
        private readonly IMapper _mapper;

        private FrozenDictionary<string, Func<BalanceStatusVM, object>> SortMap { get; }

        public GetBalanceStatuses(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;

            SortMap = new Dictionary<string, Func<BalanceStatusVM, object>>
            {
                { "firstName", x => x.FirstName },
                { "lastName", x => x.LastName },
                { "status", x => x.Status }
            }.ToFrozenDictionary();
        }

        public async Task<List<BalanceStatusVM>> Handle(Query request, CancellationToken cancellationToken)
        {
            var query = _applicationDbContext.Persons.AsNoTracking();

            if (!string.IsNullOrEmpty(request.SearchText))
            {
                query = query.Where(x =>
                    x.FirstName.ToLower().Contains(request.SearchText.ToLower()) ||
                    x.LastName.ToLower().Contains(request.SearchText.ToLower()));
            }

            var persons = await query.ToListAsync(cancellationToken);
            var balanceStatuses = _mapper.Map<List<BalanceStatusVM>>(persons);

            if (string.IsNullOrEmpty(request.SortBy))
            {
                return balanceStatuses;
            }

            var sortExpressionFound = SortMap.TryGetValue(request.SortBy, out var sortExpression);

            if (!sortExpressionFound)
            {
                throw new HttpStatusException("Invalid sort parameter sortBy.");
            }

            var sorted = (request.SortDirection ?? SortDirection.Ascending) == SortDirection.Ascending ?
                balanceStatuses.OrderBy(sortExpression) :
                balanceStatuses.OrderByDescending(sortExpression);

            return [.. sorted];
        }
    }
}