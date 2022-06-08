using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using PatikaTeleperformanceProject1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace PatikaTeleperformanceProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        List<Person> personList = new List<Person>()
        {
            new Person{Id=1, FirstName="Özgür",LastName="Kürüm", Gender=Gender.Male, BirthDate = new DateTime(1994,05,18), Email="kurumozgur@outlook.com", PhoneNumber= "+90 505 299 99 99"},
            new Person{Id=2, FirstName="Ali",LastName="Veli", Gender=Gender.Male, BirthDate = new DateTime(1997,11,23), Email="aliveli@gmail.com", PhoneNumber= "+90 532 323 23 32"},
            new Person{Id=3, FirstName="Ayşe",LastName="Çelik", Gender=Gender.Female, BirthDate = new DateTime(1994,05,18), Email="aysecelik@outlook.com", PhoneNumber= "+90 538 555 25 55"}
        };

        [HttpGet]
        public List<Person> Persons()
        {
            return personList;
        }


        [HttpGet]
        [Route("GetPerson")]
        public Person GetPerson([FromQuery] int id)
        {
            Person person = personList.FirstOrDefault(x => x.Id == id);

            return person;
        }

        [HttpPost]
        [Route("AddPerson")]
        public IActionResult AddPerson([FromBody, FromQuery, FromHeader, FromForm] Person person)
        {
            if (ModelState.IsValid)
            {
                personList.Add(person);
            }
            else
            {
                return BadRequest();
            }
            return StatusCode(200, person);
            //return Ok(person);
        }

        [HttpPatch("EditPerson/{id}")]
        public IActionResult PatchPerson(int id, [FromBody] JsonPatchDocument<Person> person)
        {
            Person updatedPerson = personList.FirstOrDefault(x => x.Id == id);
            if (person == null)
            {
                return BadRequest(ModelState);
            }

            if (updatedPerson == null)
            {
                return NotFound();
            }

            person.ApplyTo(updatedPerson, ModelState);

            if (ModelState.IsValid)
            {
                return Ok(updatedPerson);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("UpdatePerson")]
        public IActionResult UpdatePerson(Person person)
        {
            Person updatedPerson = personList.FirstOrDefault(x => x.Id == person.Id);
            if (updatedPerson == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                updatedPerson.Id = person.Id;
                updatedPerson.FirstName = person.FirstName;
                updatedPerson.LastName = person.LastName;
                updatedPerson.Gender = person.Gender;
                updatedPerson.BirthDate = person.BirthDate;
                updatedPerson.PhoneNumber = person.PhoneNumber;
                updatedPerson.Email = person.Email;
            }

            else
            {
                return BadRequest();
            }

            personList.Add(updatedPerson);
            return Ok(updatedPerson);
        }

        [HttpDelete("DeletePerson/{id}")]
        public IActionResult DeletePerson(int id)
        {
            Person deletedPerson = personList.FirstOrDefault(x => x.Id == id);
            if (deletedPerson == null)
            {
                return NotFound();
            }
            personList.Remove(deletedPerson);
            return Ok($"{id} ids deleted successfully!");
        }

        [HttpGet("SortPersons")]
        public IOrderedEnumerable<Person> SortPersons()
        {
            return personList.OrderBy(x=>x.FirstName);
        }

        [HttpGet("SearchPerson")]
        public IEnumerable<Person> ListPerson([FromQuery] string searchingName)
        {
            return personList.Where(x => x.FirstName.Contains(searchingName)).ToList();
           
        }
    }
}
