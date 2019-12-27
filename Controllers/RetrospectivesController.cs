using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RetrospectiveApi.Models;
using RetrospectiveApi.Services;
using RetrospectiveApi.ViewModels;
using Microsoft.Extensions.Logging;

namespace RetrospectiveApi.Controllers
{
    [ApiController]
    [Route("api/Retrospectives")]
    public class RetrospectivesController : ControllerBase
    {
        private readonly IRetrospectiveService _service;
        private ILogger<RetrospectivesController> _logger;

        public RetrospectivesController(IRetrospectiveService service, ILogger<RetrospectivesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetRetrospectives")]
        public IEnumerable<RetrospectiveViewModel> Get()
        {
            var retrospectiveDto= _service.GetProjects();
            var result = Mapper.Map<IEnumerable<RetrospectiveViewModel>>(retrospectiveDto);
            return result;
        }

        [HttpGet]
        [Route("GetRetrospectivesByDate")]
        public IActionResult Get(string date)
        {
            var format = new DateTime();
            if (string.IsNullOrEmpty(date))
            {
                _logger.LogInformation($"Provided date is null {date} to datetime");
                return BadRequest("Date cannot be null or empty");
            }

            try
            {
                format = DateTime.Parse(date);
  
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while parsing date {date} to datetime with error",ex.Message);
                return BadRequest("Date provided is not valid");
            }

            var retrospectiveDto = _service.GetProjectByDate(format);
            var result = Mapper.Map<IEnumerable<RetrospectiveViewModel>>(retrospectiveDto);
            if (!result.Any())
            {
                return NoContent();
            }

            return Ok(result);

        }

        [HttpPost]
        [Route("AddRetrospective")]
        public IActionResult Add([FromBody] RetrospectiveWithoutFeedback retrospective)
        {
            if (retrospective == null)
            {
                return BadRequest();
            }

            if (retrospective.Date == null || retrospective.Date.ToString("dd/MM/yyyy") =="01/01/0001")
            {
                ModelState.AddModelError("Date", "Cannot add retrospective without a date");
            }

            if (string.IsNullOrEmpty(retrospective.Participants))
            {
                ModelState.AddModelError("Participant", "Cannot add retrospective without a participant");
            }

            if (_service.ProjectNameExist(retrospective.Name))
            {
                ModelState.AddModelError("Name", "Name has to be Unique.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = Mapper.Map<ProjectDto>(retrospective);
            _service.AddProject(result);

            if (!_service.Save())
            {
                return StatusCode(500, "A Problem happened while handling your request.");
            }

            return Ok(true);
        }

        [HttpPost]
        [Route("AddFeedback")]
        public IActionResult Add(int retrospectiveId,[FromBody] FeedbackViewModel feedback)
        {
            if (feedback == null)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(feedback.Name))
            {
                ModelState.AddModelError("Name", "Cannot add a feedback without a name");
            }

            if (string.IsNullOrEmpty(feedback.Body))
            {
                ModelState.AddModelError("Body", "Cannot add a feedback without a body");
            }

            if (string.IsNullOrEmpty(feedback.Types))
            {
                ModelState.AddModelError("Type", "Cannot add a feedback without a type");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_service.ProjectExist(retrospectiveId))
            {
                _logger.LogInformation($"Retrospective with id {retrospectiveId} wasn't found when accessing feedback.'");
                return NotFound();
            }

            var result = Mapper.Map<FeedbackDto>(feedback);
            _service.AddFeedback(retrospectiveId,result);

            if (!_service.Save())
            {
                return StatusCode(500, "A Problem happened while handling your request.");
            }

            return Ok(true);
        }
    }
}