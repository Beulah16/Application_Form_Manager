using ApplicationFormManager.ApplicationFormManager.Data;
using ApplicationFormManager.ApplicationFormManager.DTOs;
using ApplicationFormManager.ApplicationFormManager.Helpers;
using ApplicationFormManager.ApplicationFormManager.Interfaces;
using ApplicationFormManager.ApplicationFormManager.Models;
using ApplicationFormManager.ApplicationFormManager.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace ApplicationFormManager.Tests
{

    public class ApplicationFormServiceIntegrationTests : IDisposable
    {
        private readonly IApplicationFormService _service;
        private readonly AppDbContext _dbContext;

        public ApplicationFormServiceIntegrationTests()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            var serviceProvider = new ServiceCollection()
            .AddDbContext<AppDbContext>(options =>
                    options.UseCosmos("AccountEndpoint = https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==", "ApplicationFormDb"))
                .AddScoped<IApplicationFormService, ApplicationFormService>()
                .BuildServiceProvider();

            _dbContext = serviceProvider.GetRequiredService<AppDbContext>();
            _service = serviceProvider.GetRequiredService<IApplicationFormService>();

            ClearDatabase();
        }

        [Fact]
        public async Task GetForms_ReturnsListOfForms()
        {
            // Arrange
            _dbContext.ApplicationForms.Add(new ApplicationForm { Id = Guid.NewGuid(), Title = "Form1", Description = "Description1" });
            _dbContext.ApplicationForms.Add(new ApplicationForm { Id = Guid.NewGuid(), Title = "Form2", Description = "Description2" });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.GetForms();

            // Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetFormById_FormExists_ReturnsForm()
        {
            // Arrange
            var formId = Guid.NewGuid();
            _dbContext.ApplicationForms.Add(new ApplicationForm { Id = formId, Title = "Form1", Description = "Description1" });
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.GetFormById(formId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(formId, result.Id);
        }

        [Fact]
        public async Task GetFormById_FormDoesNotExist_ReturnsNull()
        {
            // Arrange
            var formId = Guid.NewGuid();

            // Act
            var result = await _service.GetFormById(formId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task PostForm_CreatesForm_ReturnsForm()
        {
            // Arrange
            var requestDto = new CreateApplicationFormRequest
            {
                Title = "Form1",
                Description = "Description1",
                CustomQuestionFields = [
                new CustomQuestion
                {
                    QuestionType = "MultipleChoice",
                    QuestionText = "Initial Question",
                    Choices = new List<string> { "Choice1", "Choice2" }
                }
                ]
            };

            // Act
            var result = await _service.PostForm(requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.Equal(requestDto.Title, result.Title);
            Assert.Equal(requestDto.Description, result.Description);
            Assert.Equal(requestDto.CustomQuestionFields, result.CustomQuestionFields);
        }

        [Fact]
        public async Task UpdateForm_FormExists_UpdatesForm()
        {
            // Arrange
            var formId = Guid.NewGuid();
            var initialForm = new ApplicationForm
            {
                Id = formId,
                Title = "Initial Title",
                Description = "Initial Description",
                CustomQuestionFields = [
                new CustomQuestion
                {
                    QuestionType = "MultipleChoice",
                    QuestionText = "Initial Question",
                    Choices = new List<string> { "Choice1", "Choice2" }
                }
                ]
            };

            _dbContext.ApplicationForms.Add(initialForm);
            await _dbContext.SaveChangesAsync();

            var requestDto = new CreateApplicationFormRequest
            {
                Title = "Updated Title",
                Description = "Updated Description",
                CustomQuestionFields = [
            new CustomQuestion { QuestionType = "TrueFalse", QuestionText = "Updated Question", Choices = null }
            ]
            };

            // Act
            var result = await _service.UpdateForm(formId, requestDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(formId, result.Id);
            Assert.Equal(requestDto.Title, result.Title);
            Assert.Equal(requestDto.Description, result.Description);
            Assert.Single(result.CustomQuestionFields); // Ensure only one question remains after update

            var updatedQuestion = result.CustomQuestionFields.First();
            Assert.Equal("TrueFalse", updatedQuestion.QuestionType);
            Assert.Equal("Updated Question", updatedQuestion.QuestionText);

            // Retrieve from database to verify
            var updatedForm = await _dbContext.ApplicationForms.FindAsync(formId);
            Assert.NotNull(updatedForm);
            Assert.Equal("Updated Title", updatedForm.Title);
            Assert.Equal("Updated Description", updatedForm.Description);
            Assert.Single(updatedForm.CustomQuestionFields);
            Assert.Equal("TrueFalse", updatedForm.CustomQuestionFields.First().QuestionType);
        }

        [Fact]
        public async Task UpdateForm_FormDoesNotExist_ReturnsNull()
        {
            // Arrange
            var formId = Guid.NewGuid();
            var requestDto = new CreateApplicationFormRequest
            {
                Title = "Updated Title",
                Description = "Updated Description",
                CustomQuestionFields = [
                new CustomQuestion { QuestionType = "TrueFalse", QuestionText = "Updated Question", Choices = null }
            ]
            };

            // Act
            var result = await _service.UpdateForm(formId, requestDto);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteForm_FormExists_DeletesForm()
        {
            // Arrange
            var formId = Guid.NewGuid();
            var formToDelete = new ApplicationForm { Id = formId, Title = "FormToDelete", Description = "DescriptionToDelete" };

            _dbContext.ApplicationForms.Add(formToDelete);
            await _dbContext.SaveChangesAsync();

            // Act
            var result = await _service.DeleteForm(formId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(formId, result.Id);

            var deletedForm = await _dbContext.ApplicationForms.FindAsync(formId);
            Assert.Null(deletedForm); // Ensure the form is deleted from the database
        }

        [Fact]
        public async Task DeleteForm_FormDoesNotExist_ReturnsNull()
        {
            // Arrange
            var formId = Guid.NewGuid();

            // Act
            var result = await _service.DeleteForm(formId);

            // Assert
            Assert.Null(result);
        }


        private void ClearDatabase()
        {
            foreach (var entity in _dbContext.ApplicationForms)
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

