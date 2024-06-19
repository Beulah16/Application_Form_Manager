using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApplicationFormManager.ApplicationFormManager.Controllers
{
    [Route("api/applicationforms")]
    [ApiController]
    public class ApplicationFormController(IApplicationFormService formService) : ControllerBase
    {
        private readonly IApplicationFormService _formService = formService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _formService.GetForms());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var applicationForm = await _formService.GetFormById(id);
            if (applicationForm == null)
            {
                return NotFound();
            }

            return Ok(applicationForm);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateApplicationFormRequest requestDto)
        {
            var applicationForm = await _formService.PostForm(requestDto);

            return CreatedAtAction("GetById", new { id = applicationForm.Id }, applicationForm);
        }


        // [HttpGet("{id}/submissions")]
        // public async Task<ActionResult<IEnumerable<ApplicationSubmission>>> GetApplicationFormSubmissions(Guid id)
        // {
        //     var applicationForm = await _context.ApplicationForms.FindAsync(id);

        //     if (applicationForm == null)
        //     {
        //         return NotFound();
        //     }

        //     return await _context.ApplicationSubmissions.Where(
        //         q => q.ApplicationFormId == applicationForm.Id
        //     ).ToListAsync();
        // }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, CreateApplicationFormRequest requestDto)
        {
            var applicationForm = await _formService.UpdateForm(id, requestDto);

            if (applicationForm == null)
            {
                return NotFound();
            }

            return CreatedAtAction("GetById", new { id = applicationForm.Id }, applicationForm);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var applicationForm = await _formService.DeleteForm(id);
            if (applicationForm == null)
            {
                return NotFound();
            }

            return Ok("Deleted");
        }
    }



}