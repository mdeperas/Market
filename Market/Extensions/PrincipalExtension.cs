using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace Market.Extensions
{
	public static class PrincipalExtension
	{
		public const string UserIdClaimType = "UserId";

		public static string GetUserId(this IPrincipal principal)
		{
			var userIdClaim = (principal as ClaimsPrincipal)?
				 .Claims?
				 .FirstOrDefault(c => c.Type == UserIdClaimType);

			return userIdClaim?.Value;
		}
	}
}