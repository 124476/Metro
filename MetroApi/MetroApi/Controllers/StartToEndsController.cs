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
using MetroApi.Entities;

namespace MetroApi.Controllers
{
    public class StartToEndsController : ApiController
    {
        private MetroDatabaseEntities db = new MetroDatabaseEntities();

        // GET: api/StartToEnds
        public IHttpActionResult GetStartToEnd()
        {
            return Ok(db.StartToEnd.Select(startToEnd => new
            {
                Id = startToEnd.Id,
                StationStartId = startToEnd.StationStartId,
                StationEndId = startToEnd.StationEndId,
                BranchId = startToEnd.BranchId,
                StationStart = new
                {
                    Id = startToEnd.StationStartId,
                    Name = startToEnd.Station.Name
                },
                StationEnd = new
                {
                    Id = startToEnd.StationEndId,
                    Name = startToEnd.Station1.Name
                },
                Branch = new
                {
                    Id = startToEnd.BranchId,
                    Name = startToEnd.Branch.Name,
                    StationStartId = startToEnd.Branch.StationStartId,
                    StationEndId = startToEnd.Branch.StationEndId,
                }
            }));
        }

        // GET: api/StartToEnds/5
        [ResponseType(typeof(StartToEnd))]
        public IHttpActionResult GetStartToEnd(int id)
        {
            StartToEnd startToEnd = db.StartToEnd.Find(id);
            if (startToEnd == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Id = startToEnd.Id,
                StationStartId = startToEnd.StationStartId,
                StationEndId = startToEnd.StationEndId,
                BranchId = startToEnd.BranchId,
                StationStart = new
                {
                    Id = startToEnd.StationStartId,
                    Name = startToEnd.Station.Name
                },
                StationEnd = new
                {
                    Id = startToEnd.StationEndId,
                    Name = startToEnd.Station1.Name
                },
                Branch = new
                {
                    Id = startToEnd.BranchId,
                    Name = startToEnd.Branch.Name,
                    StationStartId = startToEnd.Branch.StationStartId,
                    StationEndId = startToEnd.Branch.StationEndId,
                }
            });
        }

        // PUT: api/StartToEnds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutStartToEnd(int id, StartToEnd startToEnd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != startToEnd.Id)
            {
                return BadRequest();
            }

            db.Entry(startToEnd).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StartToEndExists(id))
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

        // POST: api/StartToEnds
        [ResponseType(typeof(StartToEnd))]
        public IHttpActionResult PostStartToEnd(StartToEnd startToEnd)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.StartToEnd.Add(startToEnd);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = startToEnd.Id }, startToEnd);
        }

        // DELETE: api/StartToEnds/5
        [ResponseType(typeof(StartToEnd))]
        public IHttpActionResult DeleteStartToEnd(int id)
        {
            StartToEnd startToEnd = db.StartToEnd.Find(id);
            if (startToEnd == null)
            {
                return NotFound();
            }

            db.StartToEnd.Remove(startToEnd);
            db.SaveChanges();

            return Ok(startToEnd);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool StartToEndExists(int id)
        {
            return db.StartToEnd.Count(e => e.Id == id) > 0;
        }
    }
}