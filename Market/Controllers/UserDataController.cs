using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;

namespace Market.Controllers
{
	[RoutePrefix("api/UserData")]
	public class UserDataController : ApiController
    {
		private IUnitOfWork unitOfWork;

		public UserDataController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		// GET: api/UserData
		public IQueryable<UserData> GetUsersData()
		{
			return this.unitOfWork.UserDataRepository.Get().AsQueryable();
		}

        // GET: api/UserData/5
        [ResponseType(typeof(UserData))]
        public IHttpActionResult GetUserData(string id)
        {
	        UserData UserData = this.unitOfWork.UserDataRepository.GetById(id);
            if (UserData == null)
            {
                return NotFound();
            }

            return Ok(UserData);
        }

        // PUT: api/UserData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserData(string id, UserData UserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != UserData.Id)
            {
                return BadRequest();
            }

			this.unitOfWork.UserDataRepository.Update(UserData);

			try
            {
				this.unitOfWork.Save();

			}
            catch (DbUpdateConcurrencyException)
            {
                if (!UserDataExists(id))
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

        // POST: api/UserData
        [ResponseType(typeof(UserData))]
        public IHttpActionResult PostUserData(UserData UserData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			this.unitOfWork.UserDataRepository.Insert(UserData);

            try
            {
				this.unitOfWork.Save();
            }
            catch (DbUpdateException)
            {
                if (UserDataExists(UserData.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = UserData.Id }, UserData);
        }

        // DELETE: api/UserData/5
        [ResponseType(typeof(UserData))]
        public IHttpActionResult DeleteUserData(string id)
        {
	        UserData UserData = this.unitOfWork.UserDataRepository.GetById(id);
            if (UserData == null)
            {
                return NotFound();
            }

			this.unitOfWork.UserDataRepository.Delete(id);
	        ;

			this.unitOfWork.Save();

			return Ok(UserData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserDataExists(string id)
        {
	        return this.unitOfWork.UserDataRepository.GetById(id) != null;
        }
    }
}