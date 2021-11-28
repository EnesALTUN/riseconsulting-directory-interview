using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.DirectoryUsersApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DirectoryUsersController : ControllerBase
    {
        private readonly IDirectoryUsersService _directoryUsersService;
        private readonly ICommunicationInformationService _communicationInformationService;

        public DirectoryUsersController(IDirectoryUsersService directoryUsersService, ICommunicationInformationService communicationInformationService)
        {
            _directoryUsersService = directoryUsersService;
            _communicationInformationService = communicationInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDirectoryUsers()
        {
            List<DirectoryUsers> directoryUsers = await _directoryUsersService.GetAllDirectoryUsersAsync();

            if (directoryUsers is null)
                return NoContent();

            return Ok(new ApiReturn<List<DirectoryUsers>> { Success = true, Code = StatusCodes.Status200OK, Data = directoryUsers });            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectoryUser(Guid id)
        {
            DirectoryUsers directoryUser = await _directoryUsersService.GetDirectoryUserByIdAsync(id);

            if (directoryUser is null)
                return NoContent();

            return Ok(new ApiReturn<DirectoryUsers> { Success = true, Code = StatusCodes.Status200OK, Data = directoryUser });
        }

        [HttpGet("{userId}/information/{directoryUserId}")]
        public IActionResult GetDirectoryUserInformation(Guid userId, Guid directoryUserId)
        {
            DirectoryUsersInformationVM result = _directoryUsersService.GetDirectoryUsersDetail(userId, directoryUserId);

            if (result is null)
                return NoContent();

            return Ok(new ApiReturn<DirectoryUsersInformationVM> { Success = true, Code = StatusCodes.Status200OK, Data = result });
        }

        [HttpPost("{userId}/information/{directoryUserId}")]
        public async Task<IActionResult> AddDirectoryInformation(Guid userId, Guid directoryUserId, [FromBody] CommunicationInformation communicationInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            DirectoryUsers directoryUser = _directoryUsersService.GetDirectoryUserWithCriteria(filter =>
                filter.UserId == userId && filter.DirectoryUsersId == directoryUserId);

            if (directoryUser is null)
                return NoContent();

            CommunicationInformation addedCommunicationInformation =  await _communicationInformationService.AddCommunicationInformationAsync(communicationInformation);

            return Ok(new ApiReturn<CommunicationInformation> { Success = true, Code = StatusCodes.Status200OK, Data = addedCommunicationInformation });
        }

        [HttpDelete("{userId}/directory/{directoryUserId}/information/{informationId}")]
        public async Task<IActionResult> DeleteDirectoryInformation(Guid userId, Guid directoryUserId, Guid informationId)
        {
            DirectoryUsers directoryUser = _directoryUsersService.GetDirectoryUserWithCriteria(filter =>
                filter.UserId == userId && filter.DirectoryUsersId == directoryUserId);

            if (directoryUser is null)
                return NoContent();

            await _communicationInformationService.DeleteCommunicationInformationAsync(informationId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AddDirectoryUser([FromBody] DirectoryUsers directoryUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            DirectoryUsers addedDirectoryUser = await _directoryUsersService.AddDirectoryUserAsync(directoryUser);

            return CreatedAtAction("GetDirectoryUser", "DirectoryUsers", new { id = addedDirectoryUser.DirectoryUsersId }, addedDirectoryUser);
        }

        [HttpPut]
        public IActionResult UpdateDirectoryUser([FromBody] DirectoryUsers directoryUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _directoryUsersService.UpdateDirectoryUser(directoryUser);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDirectoryUser(Guid id)
        {
            await _directoryUsersService.DeleteDirectoryUserAsync(id);

            return Ok();
        }
    }
}