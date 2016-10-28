using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MarketSimulator.Repository.IRepo;
using MarketSimulator.Repository.Models;

namespace Market.Controllers
{
    public class UserModelController : ApiController
    {
		private IUnitOfWork unitOfWork;

		public UserModelController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		// GET: api/UserModel
		public IQueryable<UserModel> GetUserModels()
		{
			return this.unitOfWork.UserModelRepository.Get().AsQueryable();
		}

        // GET: api/UserModel/5
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult GetUserModel(string id)
        {
	        UserModel userModel = this.unitOfWork.UserModelRepository.GetById(id);
            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // PUT: api/UserModel/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutUserModel(string id, UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.Id)
            {
                return BadRequest();
            }

			this.unitOfWork.UserModelRepository.Update(userModel);

			try
            {
				this.unitOfWork.Save();

			}
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
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

        // POST: api/UserModel
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult PostUserModel(UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			this.unitOfWork.UserModelRepository.Insert(userModel);

            try
            {
				this.unitOfWork.Save();
            }
            catch (DbUpdateException)
            {
                if (UserModelExists(userModel.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userModel.Id }, userModel);
        }

        // DELETE: api/UserModel/5
        [ResponseType(typeof(UserModel))]
        public IHttpActionResult DeleteUserModel(string id)
        {
	        UserModel userModel = this.unitOfWork.UserModelRepository.GetById(id);
            if (userModel == null)
            {
                return NotFound();
            }

			this.unitOfWork.UserModelRepository.Delete(id);
	        ;

			this.unitOfWork.Save();

			return Ok(userModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserModelExists(string id)
        {
	        return this.unitOfWork.UserModelRepository.GetById(id) != null;
        }
    }
}