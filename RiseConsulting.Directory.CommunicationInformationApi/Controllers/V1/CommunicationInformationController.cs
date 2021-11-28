using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Entities.Models;
using System;
using System.Threading.Tasks;

namespace RiseConsulting.Directory.CommunicationInformationApi.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CommunicationInformationController : ControllerBase
    {
        private readonly ICommunicationInformationService _communicationInformationService;

        public CommunicationInformationController(ICommunicationInformationService communicationInformationService)
        {
            _communicationInformationService = communicationInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCommunicationInformations()
        {
            return Ok(await _communicationInformationService.GetAllCommunicationInformationsAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunicationInformation(Guid id)
        {
            return Ok(await _communicationInformationService.GetCommunicationInformationByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> AddCommunicationInformation([FromBody] CommunicationInformation communicationInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            CommunicationInformation addedCommunicationInformation = await _communicationInformationService.AddCommunicationInformationAsync(communicationInformation);

            return CreatedAtAction("GetCommunicationInformation", "CommunicationInformation", new { id = addedCommunicationInformation.CommunicationInformationId }, addedCommunicationInformation);
        }

        [HttpPut]
        public IActionResult UpdateCommunicationInformation([FromBody] CommunicationInformation communicationInformation)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _communicationInformationService.UpdateCommunicationInformation(communicationInformation);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommunicationInformation(Guid id)
        {
            await _communicationInformationService.DeleteCommunicationInformationAsync(id);

            return Ok();
        }
    }
}