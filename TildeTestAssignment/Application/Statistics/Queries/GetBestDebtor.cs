using MediatR;
using TildeTestAssignment.Application.Statistics.Models;

namespace TildeTestAssignment.Application.Statistics.Queries
{
    public class GetBestDebtor : IRequestHandler<GetBestDebtor.Query, BestDebtorVM>
    {
        public class Query : IRequest<BestDebtorVM>
        { }

        public Task<BestDebtorVM> Handle(Query request, CancellationToken cancellationToken)
        {
            var person
        }
    }
}