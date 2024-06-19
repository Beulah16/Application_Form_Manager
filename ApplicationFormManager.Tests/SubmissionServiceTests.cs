using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Helpers;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Models;
using ApplicationFormManager.ApplicationFormManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFormManager.Tests
{
    public class SubmissionServiceTests
    {
        private readonly ISubmissionService _service;
        private readonly AppDbContext _dbContext;

        public SubmissionServiceTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
                    options.UseCosmos("AccountEndpoint = https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "ApplicationFormDb"))
                .AddScoped<ISubmissionService, SubmissionService>()
                .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            _service = serviceProvider.GetRequiredService<ISubmissionService>();

            ClearDatabase();
        }


        [Fact]
        public async Task GetSubmissions_ReturnsListOfSubmissions()
        {
            // Arrange
            var formId = Guid.NewGuid();
            var applicationForm = new ApplicationForm
            {
                Id = formId,
                Title = "Test Form",
                Description = "Test Description"
            };

            _dbContext.ApplicationForms.Add(applicationForm);

            _dbContext.Submissions.Add(new Submission
            {
                Id = Guid.NewGuid(),
                ApplicationFormId = formId,
                PersonalDetails = new List<PersonalInformation> { new PersonalInformation { FieldName = "Name", Value = "John Doe" } },
                AdditionalDetails = new List<QuestionAnswer> { new QuestionAnswer { Question = "Q1", Answer = "A1" } }
            });
            _dbContext.Submissions.Add(new Submission
            {
                Id = Guid.NewGuid(),
                ApplicationFormId = formId,
                PersonalDetails = new List<PersonalInformation> { new PersonalInformation { FieldName = "Name", Value = "Jane Doe" } },
                AdditionalDetails = new List<QuestionAnswer> { new QuestionAnswer { Question = "Q2", Answer = "A2" } }
            });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.GetSubmissions();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetSubmissionById_SubmissionExists_ReturnsSubmission()
        {
            // Arrange
            var formId = Guid.NewGuid();
            var submissionId = Guid.NewGuid();

            var applicationForm = new ApplicationForm
            {
                Id = formId,
                Title = "Test Form",
                Description = "Test Description"
            };

            _dbContext.ApplicationForms.Add(applicationForm);

            var submission = new Submission
            {
                Id = submissionId,
                ApplicationFormId = formId,
                PersonalDetails = new List<PersonalInformation> { new PersonalInformation { FieldName = "Name", Value = "John Doe" } },
                AdditionalDetails = new List<QuestionAnswer> { new QuestionAnswer { Question = "Q1", Answer = "A1" } }
            };
            _dbContext.Submissions.Add(submission);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.GetSubmissionById(submissionId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(submissionId, result.Id);
            Assert.Equal("John Doe", result.PersonalDetails.First().Value);
            Assert.Equal("Q1", result.AdditionalDetails.First().Question);
        }

        [Fact]
        public async Task GetSubmissionById_SubmissionDoesNotExist_ReturnsNull()
        {
            // Act
            var result = await _service.GetSubmissionById(Guid.NewGuid());

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostSubmission_FormExists_CreatesSubmission()
        {
            // Arrange
            var formId = Guid.NewGuid();

            var applicationForm = new ApplicationForm
            {
                Id = formId,
                Title = "Test Form",
                Description = "Test Description"
            };

            _dbContext.ApplicationForms.Add(applicationForm);
            await _dbContext.SaveChangesAsync();

            var requestDto = new CreateSubmissionRequest
            {
                ApplicationFormId = formId,
                PersonalDetails = new List<PersonalInformation> { new PersonalInformation { FieldName = "Name", Value = "John Doe" } },
                AdditionalDetails = new List<QuestionAnswer> { new QuestionAnswer { Question = "Q1", Answer = "A1" } }
            };

            // Act
            var result = await _service.PostSubmission(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(formId, result.ApplicationFormId);
            Assert.Equal("John Doe", result.PersonalDetails.First().Value);
            Assert.Equal("Q1", result.AdditionalDetails.First().Question);

            // Verify that the submission is stored in the database
            var storedSubmission = await _dbContext.Submissions.FindAsync(result.Id);
            Assert.NotNull(storedSubmission);
        }

        [Fact]
        public async Task PostSubmission_FormDoesNotExist_ReturnsNull()
        {
            // Arrange
            var requestDto = new CreateSubmissionRequest
            {
                ApplicationFormId = Guid.NewGuid(),
                PersonalDetails = new List<PersonalInformation> { new PersonalInformation { FieldName = "Name", Value = "John Doe" } },
                AdditionalDetails = new List<QuestionAnswer> { new QuestionAnswer { Question = "Q1", Answer = "A1" } }
            };

            // Act
            var result = await _service.PostSubmission(requestDto);

            // Assert
            Assert.Null(result);
        }

        private void ClearDatabase()
        {
            foreach (var entity in _dbContext.Submissions)
            {
                _dbContext.Remove(entity);
            }
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {

            _dbContext.Dispose();
        }
    }
}
