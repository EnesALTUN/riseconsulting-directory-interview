using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.DirectoryUsersService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using System;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.DirectoryUsersApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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