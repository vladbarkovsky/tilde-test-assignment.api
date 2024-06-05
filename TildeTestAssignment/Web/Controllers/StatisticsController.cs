using MediatR;
using Microsoft.AspNetCore.Mvc;
using TildeTestAssignment.Application.Common.Pagination;
using TildeTestAssignment.Application.Statistics.Models;
using TildeTestAssignment.Application.Statistics.Queries;

namespace TildeTestAssignment.Web.Controllers
{
    public class StatisticsController : MediatRController
    {
        public StatisticsController(ISender sender) : base(sender)
        { }

        [HttpGet("[action]")]
        public async Task<ActionResult<PaginatedResult<BalanceStatusVM>>> GetBalanceStatuses([FromBody] GetBalanceStatuses.Query query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetTop()
        {
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAverageDebts()
        {
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBestDebtor()
        {
        }
    }
}