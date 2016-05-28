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
using Enviapp.Models;

namespace Enviapp.ApiControllers
{
    public class PublicacionController : ApiController
    {
        private EnviappContext db = new EnviappContext();

        // GET api/Publicacion
        public IQueryable<Envio> GetEnvios()
        {
            return db.Envios;
        }

        // GET api/Publicacion/5
        [ResponseType(typeof(Envio))]
        public IHttpActionResult GetEnvio(int id)
        {
            Envio envio = db.Envios.Find(id);
            if (envio == null)
            {
                return NotFound();
            }

            return Ok(envio);
        }

        // PUT api/Publicacion/5
        public IHttpActionResult PutEnvio(int id, Envio envio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != envio.ID)
            {
                return BadRequest();
            }

            db.Entry(envio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnvioExists(id))
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

        // POST api/Publicacion
        [ResponseType(typeof(Envio))]
        public IHttpActionResult PostEnvio(Envio envio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Envios.Add(envio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = envio.ID }, envio);
        }

        // DELETE api/Publicacion/5
        [ResponseType(typeof(Envio))]
        public IHttpActionResult DeleteEnvio(int id)
        {
            Envio envio = db.Envios.Find(id);
            if (envio == null)
            {
                return NotFound();
            }

            db.Envios.Remove(envio);
            db.SaveChanges();

            return Ok(envio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnvioExists(int id)
        {
            return db.Envios.Count(e => e.ID == id) > 0;
        }
    }
}