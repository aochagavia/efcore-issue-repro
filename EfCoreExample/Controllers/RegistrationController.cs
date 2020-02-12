using System;
using System.Threading.Tasks;
using EfCoreExample.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfCoreExample.Controllers
{
    [ApiController]
    [Route("registrations")]
    public class RegistrationController : ControllerBase
    {
        private readonly Db _db;

        public RegistrationController(Db db)
        {
            _db = db;
        }

        [HttpGet("add-empty")]
        public async Task<Guid> AddEmptyRegistration()
        {
            var registration = new Registration();
            _db.Add(registration);
            await _db.SaveChangesAsync();
            return registration.Id;
        }

        [HttpGet("add-with-contact-name")]
        public async Task<Guid> AddRegistrationWithContactName()
        {
            var registration = new Registration();
            registration.Contact.LastName = "Doe";
            _db.Add(registration);
            await _db.SaveChangesAsync();
            return registration.Id;
        }

        [HttpGet("{id}/set-contact-name")]
        public async Task SetContactName(Guid id)
        {
            var r = await _db.FindAsync<Registration>(id);
            r.Contact.LastName = "Dummy";
            await _db.SaveChangesAsync();
        }

        [HttpGet("{id}/replace-contact")]
        public async Task ReplaceContact(Guid id)
        {
            var r = await _db.FindAsync<Registration>(id);
            r.Contact = new Contact() { LastName = "Dummy" };
            await _db.SaveChangesAsync();
        }

        [HttpGet("{id}")]
        public async Task<Registration> GetRegistration(Guid id)
        {
            return await _db.FindAsync<Registration>(id);
        }
    }
}
