using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LIBORwebAPI.Models;

namespace LIBORwebAPI.Controllers
{
    public class SOFRsController : ApiController
    {
        private LIBOREntities db = new LIBOREntities();

        // GET: api/SOFRs
        public IQueryable<SOFR> GetSOFRs()
        {

         //   db.SOFRs.Where(n => n.)


            return db.SOFRs;
        }

        // GET: api/SOFRs/5
        [ResponseType(typeof(SOFR))]
        public IHttpActionResult GetSOFR(int id)
        {
            SOFR sOFR = db.SOFRs.Find(id);
            if (sOFR == null)
            {
                return NotFound();
            }

            return Ok(sOFR);
        }

        // PUT: api/SOFRs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutSOFR(int id, SOFR sOFR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sOFR.Id)
            {
                return BadRequest();
            }

            db.Entry(sOFR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOFRExists(id))
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

        // POST: api/SOFRs
        [ResponseType(typeof(SOFR))]
        public IHttpActionResult PostSOFR(SOFR sOFR)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.SOFRs.Add(sOFR);
            //db.SaveChanges();

            var sofrIndex = db.SP_INS_SOFR(0, sOFR.Date, sOFR.SOFR_Index, 0,0,0);
            return CreatedAtRoute("DefaultApi", new {id = sOFR.Id}, sOFR);

        }

        // DELETE: api/SOFRs/5
        [ResponseType(typeof(SOFR))]
        public IHttpActionResult DeleteSOFR(int id)
        {
            SOFR sOFR = db.SOFRs.Find(id);
            if (sOFR == null)
            {
                return NotFound();
            }

            db.SOFRs.Remove(sOFR);
            db.SaveChanges();

            return Ok(sOFR);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SOFRExists(int id)
        {
            return db.SOFRs.Count(e => e.Id == id) > 0;
        }
    }
}