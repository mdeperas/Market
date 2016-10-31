using System.Data.Entity.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Market.Controllers
{
	[RoutePrefix("api/Account")]
	public class AccountController : ApiController
	{
		//private AuthRepository _repo = null;
		private IUnitOfWork unitOfWork;

		public AccountController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<HttpResponseMessage> Register(UserModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
			}

			IdentityResult result = await this.unitOfWork.AuthRepository.RegisterUser(userModel);

			HttpResponseMessage errorResult = GetErrorResult(result);

			if (errorResult != null)
			{
				return errorResult;
			}

			IdentityUser user = await this.unitOfWork.AuthRepository.FindUser(userModel.UserName, userModel.Password);

			foreach (var userWallet in userModel.UserWallets)
			{
				userWallet.UserId = user.Id;

				this.unitOfWork.UserWalletRepository.Insert(userWallet);
			}

			userModel.UserData.UserId = user.Id;
			this.unitOfWork.UserDataRepository.Insert(userModel.UserData);

			this.unitOfWork.Save();

			return Request.CreateResponse(HttpStatusCode.OK, user.Id);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.unitOfWork.AuthRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		private HttpResponseMessage GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return Request.CreateResponse(HttpStatusCode.InternalServerError);
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (string error in result.Errors)
					{
						ModelState.AddModelError("", error);
					}
				}

				if (ModelState.IsValid)
				{
					Request.CreateResponse(HttpStatusCode.BadRequest);
				}

				return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
			}

			return null;
		}
	}
}