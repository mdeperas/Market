﻿using System;
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
	[Authorize]
	[RoutePrefix("api/UserWallet")]
	public class UserWalletController : ApiController
    {
		private IUnitOfWork unitOfWork;

	    public UserWalletController(IUnitOfWork unitOfWork)
	    {
		    this.unitOfWork = unitOfWork;
	    }

		// GET: api/UserWallet
		public IQueryable<UserWallet> GetUserWallets()
		{
			return this.unitOfWork.UserWalletRepository.Get().AsQueryable();
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
        public IHttpActionResult PostUserWallet(UserWallet userWallet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

			this.unitOfWork.UserWalletRepository.Insert(userWallet);

            try
            {
				this.unitOfWork.Save();
            }
            catch (DbUpdateException)
            {
                if (UserWalletExists(userWallet.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = userWallet.Id }, userWallet);
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

        private bool UserWalletExists(string id)
        {
	        return this.unitOfWork.UserWalletRepository.GetById(id) != null;
        }
    }
}