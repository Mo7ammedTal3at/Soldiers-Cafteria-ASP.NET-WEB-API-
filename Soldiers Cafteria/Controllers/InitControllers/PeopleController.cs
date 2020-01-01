using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.ModelBinding;
using AutoMapper;
using Soldiers_Cafteria.Models;
using Soldiers_Cafteria.Models.InitModels;
using Soldiers_Cafteria.Models.DTOs.Person;
using Soldiers_Cafteria.Models.DTOs.Daraga;
using Soldiers_Cafteria.Models.DTOs.Far3;

namespace Soldiers_Cafteria.Controllers.Demo
{
    public class PeopleController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/People
        [ResponseType(typeof(List<PersonDetailsDTO>))]
        public IHttpActionResult GetPeople()
        {
            List<Person> people = db.People.ToList();
            List<PersonDetailsDTO> peopleDetailsDTO = new List<PersonDetailsDTO>();
            foreach (var temp in people)
            {
                var personDTO = Mapper.Map<PersonDetailsDTO>(temp);
                personDTO.Ta2re4aMaxValue = temp.Ta2Re4A.MaxValue;
                personDTO.Ta2re4aCurrentValue = temp.Ta2Re4A.CurrentValue;
                personDTO.Ta2re4aRestValue = temp.Ta2Re4A.MaxValue-temp.Ta2Re4A.CurrentValue;
                personDTO.Daraga = temp.Daraga.Name;
                personDTO.Far3 = temp.Far3.Name;
                peopleDetailsDTO.Add(personDTO);
            }
            return Ok(peopleDetailsDTO);
        }

        // GET: api/People/5
        [ResponseType(typeof(PersonDetailsDTO))]
        public IHttpActionResult GetPerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return BadRequest("هذا الشخص غير موجود");
            }
            var personDTO = Mapper.Map<PersonDetailsDTO>(person);
            personDTO.Ta2re4aMaxValue = person.Ta2Re4A.MaxValue;
            personDTO.Ta2re4aCurrentValue = person.Ta2Re4A.CurrentValue;
            personDTO.Ta2re4aRestValue = person.Ta2Re4A.MaxValue - person.Ta2Re4A.CurrentValue;
            personDTO.Daraga = person.Daraga.Name;
            personDTO.Far3 = person.Far3.Name;
            return Ok(personDTO);
        }

        // PUT: api/People/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPerson( int id, PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorResponseMessage(ModelState));
            }
            Person personInDb = db.People.Include(p=>p.Ta2Re4A).SingleOrDefault(p => p.Id == id);
            if (personInDb == null)
            {
                return BadRequest("هذا الشخص غير موجود");
            } 
            Mapper.Map(personDTO, personInDb);
            personInDb.Ta2Re4A.MaxValue = personDTO.Ta2re4aMaxValue;
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("تم تعديل الشخص بنجاح");
        }

        // POST: api/People
        [ResponseType(typeof(PersonDTO))]
        public IHttpActionResult PostPerson(PersonDTO personDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetErrorResponseMessage(ModelState));
            }
            Person person = Mapper.Map<Person>(personDTO);
            person.AddtionTime = DateTime.Now;
            person.Ta2Re4A = new Ta2re4a
            {
                MaxValue = personDTO.Ta2re4aMaxValue,
                AddtionTime = DateTime.Now
            };
            
            db.People.Add(person);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = person.Id }, string.Format("تم إضافة {0} / {1} بنجاح", person.Daraga, person.Name));
                
        }

        // DELETE: api/People/5
        [ResponseType(typeof(Person))]
        public IHttpActionResult DeletePerson(int id)
        {
            Person person = db.People.Find(id);
            if (person == null)
            {
                return NotFound();
            }
            db.People.Remove(person);
            db.SaveChanges();
            return Ok("تم مسح الشخص بنجاح");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PersonExists(int id)
        {
            return db.People.Count(e => e.Id == id) > 0;
        }

        private string GetErrorResponseMessage(ModelStateDictionary modelState)
        {
            string responseMessage = "";
            foreach (var temp in modelState)
            {
                foreach (var error in temp.Value.Errors)
                {
                    responseMessage += error.ErrorMessage + "\n\r";
                }
            }
            return responseMessage;
        }

        // our actions

        //Get: api/People/AddPersonPage
        [Route("api/People/AddPersonPage")]
        [ResponseType(typeof(AddPersonVewModel))]
        [HttpGet]
        public IHttpActionResult AddPersonPage()
        {
            var daraga = db.Daraga.ToList();
            var far3 = db.Far3.ToList();
            var addPersonDTO = new AddPersonVewModel()
            {
                Daraga = daraga.Select(Mapper.Map<Daraga,DaragaDTO>).ToList(),
                Far3 = far3.Select(Mapper.Map<Far3, Far3DTO>).ToList()
            };
            return Ok(addPersonDTO);
        }

        //Get: api/People/Soldiers
        [Route("api/People/Soldiers")]
        [ResponseType(typeof(List<PersonDetailsDTO>))]
        [HttpGet]
        public IHttpActionResult GetSoldiers()
        {
            var soldiers = db.People.Where(p => p.DaragaId == 3).ToList().Select(Mapper.Map<Person,PersonDetailsDTO>);
            return Ok(soldiers);
        }
    }
}