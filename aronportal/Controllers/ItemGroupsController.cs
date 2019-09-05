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
    public class ItemGroupsController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/ItemGroups
        public IEnumerable<ItemGroups> GetItemGroups()
        {
            return db.ItemGroups.ToList();
        }

        // GET: api/ItemGroups/5
        [ResponseType(typeof(ItemGroups))]
        public async Task<IHttpActionResult> GetItemGroups(int id)
        {
            ItemGroups itemGroups = await db.ItemGroups.FindAsync(id);
            if (itemGroups == null)
            {
                return NotFound();
            }

            return Ok(itemGroups);
        }

        // PUT: api/ItemGroups/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutItemGroups(int id, ItemGroups itemGroups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != itemGroups.Id)
            {
                return BadRequest();
            }

            db.Entry(itemGroups).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemGroupsExists(id))
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

        // POST: api/ItemGroups
        [ResponseType(typeof(ItemGroups))]
        public async Task<IHttpActionResult> PostItemGroups(ItemGroups itemGroups)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ItemGroups.Add(itemGroups);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = itemGroups.Id }, itemGroups);
        }

        // DELETE: api/ItemGroups/5
        [ResponseType(typeof(ItemGroups))]
        public async Task<IHttpActionResult> DeleteItemGroups(int id)
        {
            ItemGroups itemGroups = await db.ItemGroups.FindAsync(id);
            if (itemGroups == null)
            {
                return NotFound();
            }

            db.ItemGroups.Remove(itemGroups);
            await db.SaveChangesAsync();

            return Ok(itemGroups);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemGroupsExists(int id)
        {
            return db.ItemGroups.Count(e => e.Id == id) > 0;
        }
    }
}