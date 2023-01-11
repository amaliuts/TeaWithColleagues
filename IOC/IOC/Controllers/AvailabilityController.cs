﻿using IOC.Constants;
using IOC.CreateModels;
using IOC.Models;
using IOC.RequestModels;
using IOC.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.ComponentModel.DataAnnotations;

namespace IOC.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityService _availabilityService;
        public AvailabilityController(IAvailabilityService availabilityService)
        {
            _availabilityService = availabilityService;
        }

        [HttpGet("get-all-availabilities")]
        public async Task<List<Availability>> GetAllAvailabilities()
        {
            return  await _availabilityService.GetAllAvailabilities();
        }


        [HttpGet("get-availabilities-by-id")]
        public async Task<ActionResult<Availability>> GetAvailability(int id)
        {
            Availability availibility = await _availabilityService.GetAvailabilityById(id);
            return availibility is not null ? availibility : NotFound(); 
        }

        [HttpGet("get-all-availabilities-by-user")]
        public async Task<ActionResult<List<Availability>>> GetAllAvailabilitiesByUser(int idUser)
        {
            return await _availabilityService.GetAllAvailabilitiesByUser(idUser);
        }
        [HttpGet("get-availabilities-by-date-and-time")]
        public async Task<List<Availability>> GetAvailabilitiesByDateAndTime(DateTime dateTime)
        {
            return await _availabilityService.GetAvailabilitiesByDateAndTime(dateTime);
        }

        [HttpGet("get-availabilities-by-type")]
        public async Task<List<Availability>> GetAvailabilitiesByType(int type)
        {
            return await _availabilityService.GetAvailabilitiesByType(type);
        }
        [HttpPut("edit-availability")]
        public async Task<bool> EditAvailability([FromBody] AvailabilityEditRequest availabilityDto)
        {
            Availability a = new Availability();
            a.IdAvailability= availabilityDto.IdAvailability;
            a.IdParticipant = availabilityDto.IdParticipant;
            a.Location = availabilityDto.Location;
            a.StartDate = availabilityDto.StartDate;
            a.IdUser = availabilityDto.IdUser;



            if (@a == null)
                return false;
            else
            {
                var result = await _availabilityService.EditAvailability(a);
                return true;
            }
        }

        [HttpPost("users/{userId}/availability")]
        public async Task<IActionResult> AddAvailability([FromBody][Required] CreateAvailabilityRequest createAvailabilityRequest, [FromRoute][Required] int userId)
        {
            CreateAvailability createAvaiability = new()
            {
                IdUser = userId,
                StartDate=createAvailabilityRequest.StartDate
            };
            return new ObjectResult(await _availabilityService.AddAvailability(createAvaiability)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpPost("users/{userId}/TeaTime")]
        public async Task<IActionResult> AddTeaTime([FromBody][Required] CreateTeaTimeRequest createTeaTimeRequest, [FromRoute][Required] int userId)
        {
            CreateAvailability createAvaiability = new()
            {
                IdUser = userId,
                StartDate = createTeaTimeRequest.StartDate
            };
            return new ObjectResult(await _availabilityService.AddAvailability(createAvaiability)) { StatusCode = StatusCodes.Status201Created };
        }

        [HttpDelete("availability/{id}")]
        public async Task<IActionResult> DeleteAvailability(int id)
        {
            bool result = await _availabilityService.DeleteAvailability(id);
            if (!result)
                return NotFound("Availability not found");
            return Ok();
        }

        [HttpPut("reschedule-availability")]
        public async Task<IActionResult> RescheduleRandomAvailability(int id, DateTime newDateTime)
        {
            bool result= await _availabilityService.RescheduleAvailability(id, newDateTime);
            if (!result)
                return NotFound("Availability not found");
            return Ok();
        }
    }
}
