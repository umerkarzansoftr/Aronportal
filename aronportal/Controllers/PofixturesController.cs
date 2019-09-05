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
    public class PofixturesController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/Pofixtures
        public IQueryable<Pofixtures> GetPofixtures()
        {
            return db.Pofixtures;
        }

        // GET: api/Pofixtures/5
        [ResponseType(typeof(Pofixtures))]
        public async Task<IHttpActionResult> GetPofixtures(int id)
        {
            Pofixtures pofixtures = await db.Pofixtures.FindAsync(id);
            if (pofixtures == null)
            {
                return NotFound();
            }

            return Ok(pofixtures);
        }

        // PUT: api/Pofixtures/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPofixtures(int id, Pofixtures pofixtures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != pofixtures.Id)
            {
                return BadRequest();
            }

            db.Entry(pofixtures).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PofixturesExists(id))
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

        // POST: api/Pofixtures
        [ResponseType(typeof(Pofixtures))]
        public async Task<IHttpActionResult> PostPofixtures(Pofixtures pofixtures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pofixtures.Add(pofixtures);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = pofixtures.Id }, pofixtures);
        }

        // DELETE: api/Pofixtures/5
        [ResponseType(typeof(Pofixtures))]
        public async Task<IHttpActionResult> DeletePofixtures(int id)
        {
            Pofixtures pofixtures = await db.Pofixtures.FindAsync(id);
            if (pofixtures == null)
            {
                return NotFound();
            }

            db.Pofixtures.Remove(pofixtures);
            await db.SaveChangesAsync();

            return Ok(pofixtures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PofixturesExists(int id)
        {
            return db.Pofixtures.Count(e => e.Id == id) > 0;
        }
    }
}