using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RiseConsulting.Directory.CommunicationInformationService.Infrastructure;
using RiseConsulting.Directory.Core.Models;
using RiseConsulting.Directory.Entities.Models;
using System;
using System.Collections.Generic;
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
            List<CommunicationInformation> communicationInformations = await _communicationInformationService.GetAllCommunicationInformationsAsync();

            if (communicationInformations is null)
                return NoContent();

            return Ok(new ApiReturn<List<CommunicationInformation>> { Success = true, Code = StatusCodes.Status200OK, Data = communicationInformations });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommunicationInformation(Guid id)
        {
            CommunicationInformation communicationInformation = await _communicationInformationService.GetCommunicationInformationByIdAsync(id);

            if (communicationInformation is null)
                return NoContent();

            return Ok(new ApiReturn<CommunicationInformation> { Success = true, Code = StatusCodes.Status200OK, Data = communicationInformation });
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