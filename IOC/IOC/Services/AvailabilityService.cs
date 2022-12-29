using IOC.DataBase;
using IOC.Models;
using IOC.RequestModels;
using IOC.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IOC.Services
{
    public class AvailabilityService : IAvailabilityService
    {
        private readonly DatabaseContext _context;

        private static List<Availability> Availabilities = new List<Availability> {
                new Availability{Id=1, StartDate = DateTime.Today, EndDate=DateTime.Today }
            };

        public AvailabilityService(DatabaseContext dataBaseContext)
        {
            _context = dataBaseContext;
        }

        public async Task<List<Availability>> GetAllAvailabilities()
        {
            return await _context.Availabilities.ToListAsync();
        }

        public async Task<Availability> GetAvailability(int id)
        {
            return await _context.Availabilities.FindAsync(id);
        }

        public async Task<int> AddAvailability(CreateAvailabilityRequest createAvailabilityRequest)
        {
            Availability availability = new Availability
            {
                StartDate = createAvailabilityRequest.StartDate,
                EndDate = createAvailabilityRequest.EndDate
            };

            _context.Add(availability);
            await _context.SaveChangesAsync();
            return availability.Id;
        }

        public async Task<bool> DeleteAvailability(int id)
        {
            Availability availability = await _context.Availabilities.FindAsync(id);
            if (availability == null)
                return false;

            _context.Availabilities.Remove(availability);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
