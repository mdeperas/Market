﻿using System.Threading.Tasks;
using System.Web.Http;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Repo;
using MarketSimulator.Repository.Models;
using Microsoft.AspNet.Identity;

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
		public async Task<IHttpActionResult> Register(UserModel userModel)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			IdentityResult result = await this.unitOfWork.AuthRepository.RegisterUser(userModel);

			IHttpActionResult errorResult = GetErrorResult(result);

			if (errorResult != null)
			{
				return errorResult;
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.unitOfWork.AuthRepository.Dispose();
			}

			base.Dispose(disposing);
		}

		private IHttpActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return InternalServerError();
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
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}