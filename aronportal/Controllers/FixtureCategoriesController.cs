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
    public class FixtureCategoriesController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/FixtureCategories
        public IEnumerable<FixtureCategories> GetFixtureCategories()
        {
            return db.FixtureCategories.ToList();
        }

        // GET: api/FixtureCategories/5
        [ResponseType(typeof(FixtureCategories))]
        public async Task<IHttpActionResult> GetFixtureCategories(int id)
        {
            FixtureCategories fixtureCategories = await db.FixtureCategories.FindAsync(id);
            if (fixtureCategories == null)
            {
                return NotFound();
            }

            return Ok(fixtureCategories);
        }

        // PUT: api/FixtureCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFixtureCategories(int id, FixtureCategories fixtureCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fixtureCategories.Id)
            {
                return BadRequest();
            }

            db.Entry(fixtureCategories).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FixtureCategoriesExists(id))
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

        // POST: api/FixtureCategories
        [ResponseType(typeof(FixtureCategories))]
        public async Task<IHttpActionResult> PostFixtureCategories(FixtureCategories fixtureCategories)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.FixtureCategories.Add(fixtureCategories);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fixtureCategories.Id }, fixtureCategories);
        }

        // DELETE: api/FixtureCategories/5
        [ResponseType(typeof(FixtureCategories))]
        public async Task<IHttpActionResult> DeleteFixtureCategories(int id)
        {
            FixtureCategories fixtureCategories = await db.FixtureCategories.FindAsync(id);
            if (fixtureCategories == null)
            {
                return NotFound();
            }

            db.FixtureCategories.Remove(fixtureCategories);
            await db.SaveChangesAsync();

            return Ok(fixtureCategories);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FixtureCategoriesExists(int id)
        {
            return db.FixtureCategories.Count(e => e.Id == id) > 0;
        }
    }
}