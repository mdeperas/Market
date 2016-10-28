using System.Web.Http;
using MarketSimulator.Repository.Models;

namespace Market.Controllers
{
	[RoutePrefix("api/Stocks")]
    public class StockController : ApiController
    {
		[Authorize]
		[Route("")]
		public IHttpActionResult Get()
		{
			return Ok();
		}
	}
}
