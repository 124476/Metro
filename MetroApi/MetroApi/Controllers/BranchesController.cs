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
    public class BranchesController : ApiController
    {
        private MetroDatabaseEntities db = new MetroDatabaseEntities();

        // GET: api/Branches
        public IHttpActionResult GetBranch()
        {
            return Ok(db.Branch.Select(branch => new
            {
                branch.Id,
                branch.Name,
                Station = new
                {
                    Id = branch.StationStartId,
                    Name = branch.Station.Name
                },
                Station1 = new
                {
                    Id = branch.StationEndId,
                    Name = branch.Station1.Name
                },
            }));
        }

        // GET: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult GetBranch(int id)
        {
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            return Ok(new
            {
                branch.Id,
                branch.Name,
                StationStart = new
                {
                    Id = branch.StationStartId,
                    Name = branch.Station.Name
                },
                StationEnd = new
                {
                    Id = branch.StationEndId,
                    Name = branch.Station1.Name
                },
            });
        }

        // PUT: api/Branches/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutBranch(int id, Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != branch.Id)
            {
                return BadRequest();
            }

            db.Entry(branch).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        [ResponseType(typeof(Branch))]
        public IHttpActionResult PostBranch(Branch branch)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Branch.Add(branch);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = branch.Id }, branch);
        }

        // DELETE: api/Branches/5
        [ResponseType(typeof(Branch))]
        public IHttpActionResult DeleteBranch(int id)
        {
            Branch branch = db.Branch.Find(id);
            if (branch == null)
            {
                return NotFound();
            }

            db.Branch.Remove(branch);
            db.SaveChanges();

            return Ok(branch);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BranchExists(int id)
        {
            return db.Branch.Count(e => e.Id == id) > 0;
        }
    }
}