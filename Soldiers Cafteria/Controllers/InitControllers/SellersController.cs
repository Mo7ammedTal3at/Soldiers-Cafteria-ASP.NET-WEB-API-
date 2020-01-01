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
using AutoMapper;
using Soldiers_Cafteria.Models;
using Soldiers_Cafteria.Models.InitModels;
using Soldiers_Cafteria.Models.DTOs.Seller;

namespace Soldiers_Cafteria.Controllers.Demo
{
    public class SellersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Sellers
        public IEnumerable<SellerDetailsDTO> GetSellers()
        {
            return db.Sellers.ToList().Select(Mapper.Map<Seller,SellerDetailsDTO>);
        }

        // GET: api/Sellers/5
        [ResponseType(typeof(SellerDetailsDTO))]
        public IHttpActionResult GetSeller(int id)
        {
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<SellerDetailsDTO>(seller));
        }

        // PUT: api/Sellers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSeller( int id,SellerDetailsDTO sellerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var sellerInDb = db.Sellers.SingleOrDefault(s => s.Id == id);
            if (sellerInDb == null)
            {
                return NotFound();
            }
            Mapper.Map(sellerDto, sellerInDb);
            //db.Entry(sellerDto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sellers
        [ResponseType(typeof(SellerDetailsDTO))]
        public IHttpActionResult PostSeller(SellerDetailsDTO sellerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var seller = Mapper.Map<Seller>(sellerDto);
            db.Sellers.Add(seller);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = seller.Id }, Mapper.Map<SellerDetailsDTO>(seller));
        }

        // DELETE: api/Sellers/5
        [ResponseType(typeof(Seller))]
        public IHttpActionResult DeleteSeller(int id)
        {
            Seller seller = db.Sellers.Find(id);
            if (seller == null)
            {
                return NotFound();
            }

            db.Sellers.Remove(seller);
            db.SaveChanges();

            return Ok(Mapper.Map<SellerDetailsDTO>(seller));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SellerExists(int id)
        {
            return db.Sellers.Count(e => e.Id == id) > 0;
        }
    }
}