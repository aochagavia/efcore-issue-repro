using System;

namespace EfCoreExample.Models
{
    public class Registration
    {
        public Guid Id { get; set; }
        public Contact Contact { get; set; }

        public Registration()
        {
            Contact = new Contact();
        }
    }
}