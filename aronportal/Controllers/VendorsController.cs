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
    public class VendorsController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/Vendors
        public IQueryable<Vendors> GetVendors()
        {
            return db.Vendors;
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(Vendors))]
        public async Task<IHttpActionResult> GetVendors(int id)
        {
            Vendors vendors = await db.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }

            return Ok(vendors);
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVendors(int id, Vendors vendors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendors.Id)
            {
                return BadRequest();
            }

            db.Entry(vendors).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendorsExists(id))
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

        // POST: api/Vendors
        [ResponseType(typeof(Vendors))]
        public async Task<IHttpActionResult> PostVendors(Vendors vendors)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vendors.Add(vendors);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = vendors.Id }, vendors);
        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(Vendors))]
        public async Task<IHttpActionResult> DeleteVendors(int id)
        {
            Vendors vendors = await db.Vendors.FindAsync(id);
            if (vendors == null)
            {
                return NotFound();
            }

            db.Vendors.Remove(vendors);
            await db.SaveChangesAsync();

            return Ok(vendors);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendorsExists(int id)
        {
            return db.Vendors.Count(e => e.Id == id) > 0;
        }
    }
}