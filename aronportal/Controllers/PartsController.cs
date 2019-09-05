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
    public class PartsController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/Parts
        public IEnumerable<Parts> GetParts()
        {
           
                return db.Parts.ToArray();
            
            
        }

        // GET: api/Parts/5
        [ResponseType(typeof(Parts))]
        public async Task<IHttpActionResult> GetParts(int id)
        {
            Parts parts = await db.Parts.FindAsync(id);
            if (parts == null)
            {
                return NotFound();
            }

            return Ok(parts);
        }

        // PUT: api/Parts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutParts(int id, Parts parts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parts.Id)
            {
                return BadRequest();
            }

            db.Entry(parts).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PartsExists(id))
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

        // POST: api/Parts
        [ResponseType(typeof(Parts))]
        public async Task<IHttpActionResult> PostParts(Parts parts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Parts.Add(parts);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = parts.Id }, parts);
        }

        // DELETE: api/Parts/5
        [ResponseType(typeof(Parts))]
        public async Task<IHttpActionResult> DeleteParts(int id)
        {
            Parts parts = await db.Parts.FindAsync(id);
            if (parts == null)
            {
                return NotFound();
            }

            db.Parts.Remove(parts);
            await db.SaveChangesAsync();

            return Ok(parts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PartsExists(int id)
        {
            return db.Parts.Count(e => e.Id == id) > 0;
        }
    }
}