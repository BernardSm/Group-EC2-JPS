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
using OnlineWebApi.Models;

namespace OnlineWebApi.Controllers
{
    public class TellersController : ApiController
    {
        private NCB_DBEntities db = new NCB_DBEntities();

        // GET: api/Tellers
        public IQueryable<Teller> GetTellers()
        {
            return db.Tellers;
        }

        // GET: api/Tellers/5
        [ResponseType(typeof(Teller))]
        public IHttpActionResult GetTeller(int id)
        {
            Teller teller = db.Tellers.Find(id);
            if (teller == null)
            {
                return NotFound();
            }

            return Ok(teller);
        }

        // PUT: api/Tellers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTeller(int id, Teller teller)
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
        public IHttpActionResult PostTeller(Teller teller)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tellers.Add(teller);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = teller.Id }, teller);
        }

        // DELETE: api/Tellers/5
        [ResponseType(typeof(Teller))]
        public IHttpActionResult DeleteTeller(int id)
        {
            Teller teller = db.Tellers.Find(id);
            if (teller == null)
            {
                return NotFound();
            }

            db.Tellers.Remove(teller);
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
            return db.Tellers.Count(e => e.Id == id) > 0;
        }
    }
}