using System;
using EfCoreExample.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EfCoreExample.Tests
{
    public class RawTests
    {
        private void EnsureDbExists()
        {
            using var db = new Db();
            db.Database.Migrate();
        }

        [Fact]
        public void Modify_Contact_LastName_IsSaved()
        {
            EnsureDbExists();

            Guid? id;
            using (var db = new Db())
            {
                var registration = new Registration();
                db.Registrations.Add(registration);
                db.SaveChanges();
                id = registration.Id;
            }

            using (var db = new Db())
            {
                var registration = db.Registrations.Find(id);
                registration.Contact.LastName = "Dummy";
                db.SaveChanges();
            }

            using (var db = new Db())
            {
                var registration = db.Registrations.Find(id);
                Assert.Equal("Dummy", registration.Contact.LastName);
            }
        }
    }
}