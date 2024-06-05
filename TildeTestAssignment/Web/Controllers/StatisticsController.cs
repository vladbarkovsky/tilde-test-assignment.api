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

        [HttpGet("[action]")]
        public async Task<ActionResult<List<BalanceStatusVM>>> GetBalanceStatuses()
        {
            return await _sender.Send(new GetBalanceStatuses.Query());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BiggestDebtorCreditorVM>> GetBiggestDebtorCreditor()
        {
            return await _sender.Send(new GetBiggestDebtorCreditor.Query());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<AverageDebtVM>>> GetAverageDebts()
        {
            return await _sender.Send(new GetAverageDebts.Query());
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<BestDebtorVM>> GetBestDebtor()
        {
            return await _sender.Send(new GetBestDebtor.Query());
        }
    }
}