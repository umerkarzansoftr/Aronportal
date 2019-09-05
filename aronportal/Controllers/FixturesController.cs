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
    public class FixturesController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/Fixtures
        public IEnumerable<Fixtures> GetFixtures()
        {
            return db.Fixtures.ToList(); 
        }

        // GET: api/Fixtures/5
        [ResponseType(typeof(Fixtures))]
        public async Task<IHttpActionResult> GetFixtures(int id)
        {
            Fixtures fixtures = await db.Fixtures.FindAsync(id);
            if (fixtures == null)
            {
                return NotFound();
            }

            return Ok(fixtures);
        }

        // PUT: api/Fixtures/5
        [ResponseType(typeof(void))]
        public void PutFixtures(int id, Fixtures fixtures)
        {
            var result = db.Fixtures.Include(e => e.FixtureParts).FirstOrDefault(v => v.Id == fixtures.Id);
            result.Id = fixtures.Id;
            result.FixtureName = fixtures.FixtureName;
            result.FixtureCategoryId = fixtures.FixtureCategoryId;
            result.FixtureCode = fixtures.FixtureCode;


            var lineToRemove = result.FixtureParts
                              .Where(line =>
                                  !(fixtures.FixtureParts.Any(inputLine => inputLine.Id == line.Id))
                              ).ToList();

            db.FixtureParts.RemoveRange(lineToRemove);

            foreach (var line in fixtures.FixtureParts)
            {
                if (line.Id > 0)
                {
                    var lineToModify = db.FixtureParts.Where(V => V.Id == line.Id).FirstOrDefault();
                    lineToModify.Id = line.Id;
                    lineToModify.PartId = line.PartId;
                    lineToModify.Quantity = line.Quantity;
                    lineToModify.FixtureId = line.FixtureId;
                    //lineToModify.Voucher = line.Voucher;
                }
                else
                {
                    result.FixtureParts.Add(new FixtureParts
                    {
                        PartId = line.PartId,
                        FixtureId = line.FixtureId,
                        Cost = line.Cost,
                        Quantity = line.Quantity

                    });
                }
            }
            db.SaveChanges();
        }

        // POST: api/Fixtures
        [ResponseType(typeof(Fixtures))]
        public async Task<IHttpActionResult> PostFixtures(Fixtures fixtures)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fixtures.Add(fixtures);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = fixtures.Id }, fixtures);
        }

        // DELETE: api/Fixtures/5
        [ResponseType(typeof(Fixtures))]
        public async Task<IHttpActionResult> DeleteFixtures(int id)
        {
            Fixtures fixtures = await db.Fixtures.FindAsync(id);
            if (fixtures == null)
            {
                return NotFound();
            }

            db.Fixtures.Remove(fixtures);
            await db.SaveChangesAsync();

            return Ok(fixtures);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FixturesExists(int id)
        {
            return db.Fixtures.Count(e => e.Id == id) > 0;
        }
    }
}