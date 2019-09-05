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
    public class FixturePartsController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/FixtureParts
        public  IEnumerable<FixtureParts> GetFixtureParts()
        {
            return db.FixtureParts.ToList();
        }

        // GET: api/FixtureParts/5
        [ResponseType(typeof(FixtureParts))]
        public async Task<IHttpActionResult> GetFixtureParts(int id)
        {
            FixtureParts fixtureParts = await db.FixtureParts.FindAsync(id);
            if (fixtureParts == null)
            {
                return NotFound();
            }

            return Ok(fixtureParts);
        }

        // PUT: api/FixtureParts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFixtureParts(int id, FixtureParts fixtureParts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fixtureParts.Id)
            {
                return BadRequest();
            }

            db.Entry(fixtureParts).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixturePartsExists(id))
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

        // POST: api/FixtureParts
        [ResponseType(typeof(FixtureParts))]
        public async Task<IHttpActionResult> PostFixtureParts(FixtureParts fixtureParts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FixtureParts.Add(fixtureParts);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fixtureParts.Id }, fixtureParts);
        }

        // DELETE: api/FixtureParts/5
        [ResponseType(typeof(FixtureParts))]
        public async Task<IHttpActionResult> DeleteFixtureParts(int id)
        {
            FixtureParts fixtureParts = await db.FixtureParts.FindAsync(id);
            if (fixtureParts == null)
            {
                return NotFound();
            }

            db.FixtureParts.Remove(fixtureParts);
            await db.SaveChangesAsync();

            return Ok(fixtureParts);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FixturePartsExists(int id)
        {
            return db.FixtureParts.Count(e => e.Id == id) > 0;
        }
    }
}