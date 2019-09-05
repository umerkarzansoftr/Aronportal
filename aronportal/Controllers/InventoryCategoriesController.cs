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
    public class InventoryCategoriesController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/InventoryCategories
        public IEnumerable<InventoryCategory> GetInventoryCategory()
        {
            return db.InventoryCategory.ToList();
        }

        // GET: api/InventoryCategories/5
        [ResponseType(typeof(InventoryCategory))]
        public async Task<IHttpActionResult> GetInventoryCategory(int id)
        {
            InventoryCategory inventoryCategory = await db.InventoryCategory.FindAsync(id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }

            return Ok(inventoryCategory);
        }

        // PUT: api/InventoryCategories/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutInventoryCategory(int id, InventoryCategory inventoryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != inventoryCategory.Id)
            {
                return BadRequest();
            }

            db.Entry(inventoryCategory).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InventoryCategoryExists(id))
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

        // POST: api/InventoryCategories
        [ResponseType(typeof(InventoryCategory))]
        public async Task<IHttpActionResult> PostInventoryCategory(InventoryCategory inventoryCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.InventoryCategory.Add(inventoryCategory);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = inventoryCategory.Id }, inventoryCategory);
        }

        // DELETE: api/InventoryCategories/5
        [ResponseType(typeof(InventoryCategory))]
        public async Task<IHttpActionResult> DeleteInventoryCategory(int id)
        {
            InventoryCategory inventoryCategory = await db.InventoryCategory.FindAsync(id);
            if (inventoryCategory == null)
            {
                return NotFound();
            }

            db.InventoryCategory.Remove(inventoryCategory);
            await db.SaveChangesAsync();

            return Ok(inventoryCategory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool InventoryCategoryExists(int id)
        {
            return db.InventoryCategory.Count(e => e.Id == id) > 0;
        }
    }
}