using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MetroApi.Entities;

namespace MetroApi.Controllers
{
    public class TrainsController : ApiController
    {
        private MetroDatabaseEntities db = new MetroDatabaseEntities();

        // GET: api/Trains
        public IHttpActionResult GetTrain()
        {
            return Ok(db.Train.Select(x => new
            {
                Id = x.Id,
                IsUp = x.IsUp,
                IsRun = x.IsRun,
                StationId = x.StationId,
                BranchId = x.BranchId,
                Station = new
                {
                    Id = x.StationId,
                    Name = x.Station.Name
                },
                Branch = new
                {
                    Id = x.BranchId,
                    Name = x.Branch.Name,
                    StationStartId = x.Branch.StationStartId,
                    StationEndId = x.Branch.StationEndId,
                    StationStart = new
                    {
                        Id = x.Branch.StationStartId,
                        Name = x.Branch.Station.Name
                    },
                    StationEnd = new
                    {
                        Id = x.Branch.StationEndId,
                        Name = x.Branch.Station1.Name
                    },
                }
            }));
        }

        // GET: api/Trains/5
        [ResponseType(typeof(Train))]
        public IHttpActionResult GetTrain(int id)
        {
            Train train = db.Train.Find(id);
            if (train == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                Id = train.Id,
                IsUp = train.IsUp,
                IsRun = train.IsRun,
                StationId = train.StationId,
                BranchId = train.BranchId,
                Station = new
                {
                    Id = train.StationId,
                    Name = train.Station.Name
                },
                Branch = new
                {
                    Id = train.BranchId,
                    Name = train.Branch.Name,
                    StationStartId = train.Branch.StationStartId,
                    StationEndId = train.Branch.StationEndId,
                    StationStart = new
                    {
                        Id = train.Branch.StationStartId,
                        Name = train.Branch.Station.Name
                    },
                    StationEnd = new
                    {
                        Id = train.Branch.StationEndId,
                        Name = train.Branch.Station1.Name
                    },
                }
            });
        }

        // PUT: api/Trains/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTrain(int id, Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != train.Id)
            {
                return BadRequest();
            }

            db.Entry(train).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TrainExists(id))
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

        // POST: api/Trains
        [ResponseType(typeof(Train))]
        public IHttpActionResult PostTrain(Train train)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Train.Add(train);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = train.Id }, train);
        }

        // DELETE: api/Trains/5
        [ResponseType(typeof(Train))]
        public IHttpActionResult DeleteTrain(int id)
        {
            Train train = db.Train.Find(id);
            if (train == null)
            {
                return NotFound();
            }

            db.Train.Remove(train);
            db.SaveChanges();

            return Ok(train);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TrainExists(int id)
        {
            return db.Train.Count(e => e.Id == id) > 0;
        }
    }
}