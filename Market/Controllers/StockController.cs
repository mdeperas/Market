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
	//[Authorize]
    public class StockController : ApiController
    {
		private IUnitOfWork unitOfWork;

		public StockController(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork;
		}

		// GET: api/Stock
		public IQueryable<Stock> GetStocks()
		{
			return this.unitOfWork.StockRepository.Get().AsQueryable();
		}

        // GET: api/Stock/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult GetStock(string id)
        {
	        Stock stock = this.unitOfWork.StockRepository.GetById(id);
            if (stock == null)
            {
                return NotFound();
            }

            return Ok(stock);
        }

        // PUT: api/Stock/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStock(string id, Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stock.Id)
            {
                return BadRequest();
            }

            this.unitOfWork.StockRepository.Update(stock);

            try
            {
                this.unitOfWork.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StockExists(id))
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

        // POST: api/Stock
        [ResponseType(typeof(Stock))]
        public IHttpActionResult PostStock(Stock stock)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.unitOfWork.StockRepository.Insert(stock);

            try
            {
                this.unitOfWork.Save();
            }
            catch (DbUpdateException)
            {
                if (StockExists(stock.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = stock.Id }, stock);
        }

        // DELETE: api/Stock/5
        [ResponseType(typeof(Stock))]
        public IHttpActionResult DeleteStock(string id)
        {
	        Stock stock = this.unitOfWork.StockRepository.GetById(id);
            if (stock == null)
            {
                return NotFound();
            }

            this.unitOfWork.StockRepository.Delete(id);
            this.unitOfWork.Save();

            return Ok(stock);
        }

       private bool StockExists(string id)
        {
	        return this.unitOfWork.StockRepository.GetById(id) != null;
        }
    }
}