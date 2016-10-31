using System;
using System.Data.Entity;
using System.Threading.Tasks;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;
using MarketSimulator.Repository.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MarketSimulator.Repository.Repo
{
    public class AuthRepository : IDisposable
	{
		private IMarketSimulatorContext context;
		private UserManager<IdentityUser> _userManager;

		public AuthRepository(IMarketSimulatorContext context)
		{
			this.context = context;
			_userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(this.context as DbContext));
		}

		public async Task<IdentityResult> RegisterUser(UserModel userModel)
		{
			IdentityUser user = new IdentityUser
			{
				UserName = userModel.UserName
			};

			var result = await _userManager.CreateAsync(user, userModel.Password);

			return result;
		}

		public async Task<IdentityUser> FindUser(string userName, string password)
		{
			IdentityUser user = await _userManager.FindAsync(userName, password);

			return user;
		}

		public void Dispose()
		{
			context.Dispose();
			_userManager.Dispose();
		}
	}
}