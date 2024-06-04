using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TildeTestAssignment.Web.Controllers
{
    public class StatisticsController : MediatRController
    {
        public StatisticsController(ISender sender) : base(sender)
        { }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetBalanceStatuses()
        {
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