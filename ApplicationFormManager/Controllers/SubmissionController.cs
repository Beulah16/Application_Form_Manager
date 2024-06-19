using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFormManager.ApplicationFormManager.Controllers
{
    [Route("api/submissions")]
    [ApiController]
    public class SubmissionController(ISubmissionService submissionService, AppDbContext context) : ControllerBase
    {
        private readonly ISubmissionService _submissionService = submissionService;
        private readonly AppDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _submissionService.GetSubmissions());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var submission = await _submissionService.GetSubmissionById(id);

            if (submission == null)
            {
                return NotFound();
            }

            return Ok(submission);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateSubmissionRequest requestDto)
        {
            var applicationSubmission = await _submissionService.PostSubmission(requestDto);
            if (applicationSubmission == null) return NotFound("Form does not exist");

            return CreatedAtAction("GetById", new { id = applicationSubmission.Id }, applicationSubmission);
        }
    }
}