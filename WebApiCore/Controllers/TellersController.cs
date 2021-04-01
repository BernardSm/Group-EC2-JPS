using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiCore.Model;

namespace WebApiCore.Controllers
{
    
    public class TellersController : ApiController
    {
        private NCBDataContext db = new NCBDataContext(null);

        // GET: api/Tellers
        public IQueryable<Teller> GetTellerss()
        {
            return db.Teller;
        }

        // GET: api/Tellers/5
        [ResponseType(typeof(Teller))]
        public IHttpActionResult GetTeller(int id)
        {
            Teller teller = db.Teller.Find(id);
            if (teller == null)
            {
                return NotFound();
            }

            return Ok(teller);
        }

        // PUT: api/Tellers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Teller(int id, Teller teller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teller.Id)
            {
                return BadRequest();
            }

            db.Entry(teller).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TellerExists(id))
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

        // POST: api/Tellers
        [ResponseType(typeof(Teller))]
        public IHttpActionResult PostCustomer(Teller teller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Teller.Add(teller);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teller.Id }, teller);
        }

        // DELETE: api/Tellers/5
        [ResponseType(typeof(Teller))]
        public IHttpActionResult DeleteTeller(int id)
        {
            Teller teller = db.Teller.Find(id);
            if (teller == null)
            {
                return NotFound();
            }

            db.Teller.Remove(teller);
            db.SaveChanges();

            return Ok(teller);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TellerExists(int id)
        {
            return db.Teller.Count(e => e.Id == id) > 0;
        }
    }
}