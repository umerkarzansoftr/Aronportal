using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using aronportal.Models;

namespace aronportal.Controllers
{
    public class PopartsController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/Poparts
        public IQueryable<Poparts> GetPoparts()
        {
            return db.Poparts;
        }

        // GET: api/Poparts/5
        [ResponseType(typeof(Poparts))]
        public async Task<IHttpActionResult> GetPoparts(int id)
        {
            Poparts poparts = await db.Poparts.FindAsync(id);
            if (poparts == null)
            {
                return NotFound();
            }

            return Ok(poparts);
        }

        // PUT: api/Poparts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPoparts(int id, Poparts poparts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poparts.Id)
            {
                return BadRequest();
            }

            db.Entry(poparts).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopartsExists(id))
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

        // POST: api/Poparts
        [ResponseType(typeof(Poparts))]
        public async Task<IHttpActionResult> PostPoparts(Poparts poparts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Poparts.Add(poparts);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = poparts.Id }, poparts);
        }

        // DELETE: api/Poparts/5
        [ResponseType(typeof(Poparts))]
        public async Task<IHttpActionResult> DeletePoparts(int id)
        {
            Poparts poparts = await db.Poparts.FindAsync(id);
            if (poparts == null)
            {
                return NotFound();
            }

            db.Poparts.Remove(poparts);
            await db.SaveChangesAsync();

            return Ok(poparts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PopartsExists(int id)
        {
            return db.Poparts.Count(e => e.Id == id) > 0;
        }
    }
}