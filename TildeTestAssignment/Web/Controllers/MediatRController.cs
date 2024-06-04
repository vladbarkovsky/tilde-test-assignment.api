using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace TildeTestAssignment.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MediatRController : ControllerBase
    {
        protected readonly ISender _sender;

        public MediatRController(ISender sender)
        {
            _sender = sender;
        }
    }
}
