using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using EfCoreExample.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace EfCoreExample.Tests
{
    public class RegistrationTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public RegistrationTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        // This test fails in EF Core 3.3.1, but passes in EF Core 2.2.6
        [Fact]
        public async Task AddAndModifyRegistration_UsingAssignment_ShouldUpdateIt()
        {
            var client = _factory.CreateClient();
            var registrationId = await Get<Guid>(client, "registrations/add-empty");
            Assert.NotEqual(Guid.Empty, registrationId);

            var response = await client.GetAsync($"registrations/{registrationId}/set-contact-name");
            response.EnsureSuccessStatusCode();

            var registration = await Get<Registration>(client, $"registrations/{registrationId}");
            Assert.Equal("Dummy", registration.Contact.LastName);
        }

        [Fact]
        public async Task AddAndModifyRegistration_UsingReplacement_ShouldUpdateIt()
        {
            var client = _factory.CreateClient();
            var registrationId = await Get<Guid>(client, "registrations/add-empty");
            Assert.NotEqual(Guid.Empty, registrationId);

            var response = await client.GetAsync($"registrations/{registrationId}/replace-contact");
            response.EnsureSuccessStatusCode();

            var registration = await Get<Registration>(client, $"registrations/{registrationId}");
            Assert.Equal("Dummy", registration.Contact.LastName);
        }

        [Fact]
        public async Task AddRegistrationWithContactName_ShouldUpdateIt()
        {
            var client = _factory.CreateClient();
            var registrationId = await Get<Guid>(client, "registrations/add-with-contact-name");
            Assert.NotEqual(Guid.Empty, registrationId);

            var response = await client.GetAsync($"registrations/{registrationId}/set-contact-name");
            response.EnsureSuccessStatusCode();

            var registration = await Get<Registration>(client, $"registrations/{registrationId}");
            Assert.Equal("Dummy", registration.Contact.LastName);
        }

        private static async Task<T> Get<T>(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var bytes = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(bytes, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }
    }
}