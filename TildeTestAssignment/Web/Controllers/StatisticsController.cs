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
        [EndpointDescription("1. Par katru cilvēku noskaidro vai viņa bilance ir pozitīva vai negatīva")]
        public async Task<ActionResult<PaginatedResult<BalanceStatusVM>>> GetBalanceStatuses([FromBody] GetBalanceStatuses.Query query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("[action]")]
        [EndpointDescription("2. Atrod pašu liekāko parādnieku un aizdevēju")]
        public async Task<ActionResult<BiggestDebtorCreditorVM>> GetTop()
        {
            return await _sender.Send(new GetBiggestDebtorCreditor.Query());
        }

        [HttpGet("[action]")]
        [EndpointDescription("3. Atrod katra cilvēka vidējo aizņēmumu lielumu")]
        public async Task<ActionResult<PaginatedResult<AverageDebtVM>>> GetAverageDebts([FromBody] GetAverageDebts.Query query)
        {
            return await _sender.Send(query);
        }

        [HttpGet("[action]")]
        [EndpointDescription("4. Atrod vislabāko aizņēmēju, t.i., to, kurš ir 1) vislielāko procentu no aizņemtā ir atdevis un 2) vislielāko kopsummu aizņēmies.")]
        public async Task<ActionResult> GetBestDebtor()
        {
        }
    }
}