using IOC.Models;
using IOC.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace IOC.Services.Interfaces
{
    public interface IAvailabilityService
    {
        Task<List<Availability>> GetAllAvailabilities();

        Task<Availability> GetAvailability(int id);

        Task<int> AddAvailability(CreateAvailabilityRequest createAvailabilityRequest);

        Task<bool> DeleteAvailability(int id);
    }
}
