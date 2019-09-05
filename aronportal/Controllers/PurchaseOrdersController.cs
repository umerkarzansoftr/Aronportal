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
    public class PurchaseOrdersController : ApiController
    {
        private AronPortalDbContext db = new AronPortalDbContext();

        // GET: api/PurchaseOrders
        public IEnumerable<PurchaseOrders> GetPurchaseOrders()
        {
            return db.PurchaseOrders.ToList();
        }

        // GET: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrders))]
        public async Task<IHttpActionResult> GetPurchaseOrders(int id)
        {
            PurchaseOrders purchaseOrders = await db.PurchaseOrders.FindAsync(id);
            if (purchaseOrders == null)
            {
                return NotFound();
            }

            return Ok(purchaseOrders);
        }

        // PUT: api/PurchaseOrders/5
        [ResponseType(typeof(void))]
        public void PutPurchaseOrders(int id, [FromBody] PurchaseOrders input)
        {
            var result = db.PurchaseOrders.Include(e => e.Pofixtures).Include(e => e.Poparts).FirstOrDefault(v => v.Id == input.Id);
            result.Id = input.Id;
            result.PoactShipDate = input.PoactShipDate;
            result.Pocompleted = input.Pocompleted;
            result.Pocost = input.Pocost;
            result.PoestShipDate = input.PoestShipDate;
            result.Poname = input.Poname;
            result.Ponumber = input.Ponumber;
            result.PorecDate = input.PorecDate;
            result.Poprice = input.Poprice;


            var lineToRemove = result.Poparts
                              .Where(line =>
                                  !(input.Poparts.Any(inputLine => inputLine.Id == line.Id))
                              ).ToList();

            db.Poparts.RemoveRange(lineToRemove);
            var lineToRemove2 = result.Pofixtures
                            .Where(line =>
                                !(input.Pofixtures.Any(inputLine => inputLine.Id == line.Id))
                            ).ToList();

            db.Pofixtures.RemoveRange(lineToRemove2);

            foreach (var line in input.Pofixtures)
            {
                if (line.Id > 0)
                {
                    var lineToModify = db.Pofixtures.Where(V => V.Id == line.Id).FirstOrDefault();
                    lineToModify.FixtureId = line.FixtureId;
                    lineToModify.FixtureCommision = line.FixtureCommision;
                    lineToModify.FixtureCost = line.FixtureCost;
                    lineToModify.FixtureQuantity = line.FixtureQuantity;
                    lineToModify.FixturePrice = line.FixturePrice;
                    lineToModify.PurchaseOrderId = result.Id;
                    lineToModify.FixtureQuantityCompleted = line.FixtureQuantityCompleted;
                    lineToModify.FixtureQuantityShipped = line.FixtureQuantityShipped;
                    //lineToModify.Voucher = line.Voucher;
                }
                else
                {
                    result.Pofixtures.Add(new Pofixtures
                    {
                        FixtureId = line.FixtureId,
                        FixtureCommision = line.FixtureCommision,
                        FixtureCost = line.FixtureCost,
                        FixtureQuantity = line.FixtureQuantity,
                        FixturePrice = line.FixturePrice,
                        PurchaseOrderId = result.Id,
                        FixtureQuantityCompleted = line.FixtureQuantityCompleted,
                        FixtureQuantityShipped = line.FixtureQuantityShipped

                    });
                }
            }
            foreach (var line in input.Poparts)
            {

                if (line.Id > 0)
                {
                    var lineToModify = db.Poparts.Where(V => V.Id == line.Id).FirstOrDefault();
                    lineToModify.PurchaseOrderId = line.PurchaseOrderId;
                    lineToModify.PartId = line.PartId;
                    lineToModify.Quantity = line.Quantity;
                    //lineToModify.Voucher = line.Voucher;
                }
                else
                {
                    result.Poparts.Add(new Poparts
                    {
                        PurchaseOrderId = result.Id,
                        PartId = line.PartId,
                        Quantity = line.Quantity,

                    });
                }

            }
            db.SaveChanges();

        }

        // POST: api/PurchaseOrders
        [ResponseType(typeof(PurchaseOrders))]
        public void PostPurchaseOrders([FromBody] PurchaseOrders input)
        {
            var result = new PurchaseOrders();

            result.PoactShipDate = input.PoactShipDate;
            result.Pocompleted = input.Pocompleted;
            result.Pocost = input.Pocost;
            result.PoestShipDate = input.PoestShipDate;
            result.Poname = input.Poname;
            result.Ponumber = input.Ponumber;
            result.PorecDate = input.PorecDate;
            result.Poprice = input.Poprice;
            db.PurchaseOrders.Add(result);
            db.SaveChanges();



            foreach (var line in input.Pofixtures)
            {

                result.Pofixtures.Add(new Pofixtures
                {
                    FixtureId = line.FixtureId,
                    FixtureCommision = line.FixtureCommision,
                    FixtureCost = line.FixtureCost,
                    FixtureQuantity = line.FixtureQuantity,
                    FixturePrice = line.FixturePrice,
                    PurchaseOrderId = result.Id,
                    FixtureQuantityCompleted = line.FixtureQuantityCompleted,
                    FixtureQuantityShipped = line.FixtureQuantityShipped

                });

            }
            foreach (var line in input.Poparts)
            {

                result.Poparts.Add(new Poparts
                {

                    PurchaseOrderId = result.Id,
                    PartId = line.PartId,
                    Quantity = line.Quantity,


                });

            }
           db.SaveChanges();

        }

        // DELETE: api/PurchaseOrders/5
        [ResponseType(typeof(PurchaseOrders))]
        public async Task<IHttpActionResult> DeletePurchaseOrders(int id)
        {
            PurchaseOrders purchaseOrders = await db.PurchaseOrders.FindAsync(id);
            if (purchaseOrders == null)
            {
                return NotFound();
            }

            db.PurchaseOrders.Remove(purchaseOrders);
            await db.SaveChangesAsync();

            return Ok(purchaseOrders);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PurchaseOrdersExists(int id)
        {
            return db.PurchaseOrders.Count(e => e.Id == id) > 0;
        }
    }
}