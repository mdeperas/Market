using System.Web.Http;

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
