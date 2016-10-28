using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using MarketSimulator.Repository.IRepo;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security.OAuth;

namespace Market.Providers
{
	public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
	{
		private IUnitOfWork unitOfWork;
		public SimpleAuthorizationServerProvider(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
		{
			context.Validated();
		}

		public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
		{
			context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new [] {"*"});

			IdentityUser user = await this.unitOfWork.AuthRepository.FindUser(context.UserName, context.Password);

			if (user == null)
			{
				context.SetError("invalid_grant", "The username or password is incorrect");
				return;
			}

			var identity = new ClaimsIdentity(context.Options.AuthenticationType);
			identity.AddClaim(new Claim("sub", context.UserName));
			identity.AddClaim(new Claim("role", "user"));

			context.Validated(identity);
		}
	}
}