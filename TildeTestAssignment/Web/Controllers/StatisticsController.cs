using MediatR;
using Microsoft.AspNetCore.Mvc;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.Application.Statistics.Queries;

namespace TildeTestAssignment.Web.Controllers
{
    public class StatisticsController : MediatRController
    {
        public StatisticsController(ISender sender) : base(sender)
        { }

        [HttpPost("[action]")]
        public async Task<ActionResult<List<BalanceStatusVM>>> GetBalanceStatuses([FromBody] GetBalanceStatuses.Query query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BiggestDebtorCreditorVM>> GetBiggestDebtorCreditor()
        {
            return await _sender.Send(new GetBiggestDebtorCreditor.Query());
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<List<AverageDebtVM>>> GetAverageDebts([FromBody] GetAverageDebts.Query query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BestDebtorVM>> GetBestDebtor()
        {
            return await _sender.Send(new GetBestDebtor.Query());
        }
    }
}