using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;

namespace Market.Controllers
{
	[RoutePrefix("api/UserWallet")]
	public class UserWalletController : ApiController
    {
		private IUnitOfWork unitOfWork;

	    public UserWalletController(IUnitOfWork unitOfWork)
	    {
		    this.unitOfWork = unitOfWork;
	    }


		// GET: api/UserWallet
		public IQueryable<UserWallet> GetUserWallets(string userId)
		{
			return this.unitOfWork.UserWalletRepository.Get().Where(x => x.UserData.Id == userId).AsQueryable();
		}

        // GET: api/UserWallet/5
        [ResponseType(typeof(UserWallet))]
        public IHttpActionResult GetUserWallet(string id)
        {
	        UserWallet userWallet = this.unitOfWork.UserWalletRepository.GetById(id);
            if (userWallet == null)
            {
                return NotFound();
            }

            return Ok(userWallet);
        }

        // PUT: api/UserWallet/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserWallet(string id, UserWallet userWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userWallet.Id)
            {
                return BadRequest();
            }

			this.unitOfWork.UserWalletRepository.Update(userWallet);

            try
            {
				this.unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWalletExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserWallet
        [ResponseType(typeof(UserWallet))]
        public IHttpActionResult PostUserWallet([FromBody] UserWallet[] userWallets)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

	        foreach (var userWallet in userWallets)
	        {
				this.unitOfWork.UserWalletRepository.Insert(userWallet);
			}
			
			this.unitOfWork.Save();

	        return Ok();
        }

        // DELETE: api/UserWallet/5
        [ResponseType(typeof(UserWallet))]
        public IHttpActionResult DeleteUserWallet(string id)
        {
	        UserWallet userWallet = this.unitOfWork.UserWalletRepository.GetById(id);
            if (userWallet == null)
            {
                return NotFound();
            }

			this.unitOfWork.UserWalletRepository.Delete(id);
            this.unitOfWork.Save();

            return Ok(userWallet);
        }

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.unitOfWork.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool UserWalletExists(string id)
        {
	        return this.unitOfWork.UserWalletRepository.GetById(id) != null;
        }
    }
}