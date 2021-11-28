using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using RiseConsulting.Directory.Entities.ViewModels;
using System;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.DirectoryUsersApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DirectoryUsersController : ControllerBase
    {
        private readonly IDirectoryUsersService _directoryUsersService;

        public DirectoryUsersController(IDirectoryUsersService directoryUsersService)
        {
            _directoryUsersService = directoryUsersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDirectoryUsers()
        {
            return Ok(await _directoryUsersService.GetAllDirectoryUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDirectoryUser(Guid id)
        {
            return Ok(await _directoryUsersService.GetDirectoryUserByIdAsync(id));
        }

        [HttpGet("{userId}/information/{directoryUserId}")]
        public IActionResult GetDirectoryUserInformation(Guid userId, Guid directoryUserId)
        {
            DirectoryUsersInformationVM result = _directoryUsersService.GetDirectoryUsersDetail(userId, directoryUserId);

            if (result is null)
                return NoContent();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDirectoryUser([FromBody] DirectoryUsers directoryUser)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            DirectoryUsers addedDirectoryUser = await _directoryUsersService.AddDirectoryUserAsync(directoryUser);

            return CreatedAtAction("GetDirectoryUser", "DirectoryUser", new { id = addedDirectoryUser.DirectoryUsersId }, addedDirectoryUser);
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