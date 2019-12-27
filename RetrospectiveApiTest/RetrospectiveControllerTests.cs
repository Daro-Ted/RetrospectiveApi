using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RetrospectiveApi.Controllers;
using RetrospectiveApi.MappingProfile;
using RetrospectiveApi.Services;
using RetrospectiveApi.ViewModels;
using System;
using Xunit;

namespace RetrospectiveApiTest
{
    public class RetrospectiveInsertTest
    {

        public void Dispose()
        {
            Mapper.Reset();
        }

        [Fact]
        public void GetRetrospective()
        {
            //Arrange
            Mapper.Reset();
            Mapper.Initialize(cfg => { cfg.AddProfile<MapProfile>(); });
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);

            //Act
            var result = controller.Get();

            //Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetRetrospectiveByDateInvalidDate()
        {
            //Arrange
            Mapper.Reset();
            Mapper.Initialize(cfg => { cfg.AddProfile<MapProfile>(); });
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);
            var date = "12/01/201N";

            //Act
            var result = controller.Get(date);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }


        [Fact]
        public void GetRetrospectiveByDateNoContent()
        {
            //Arrange
            Mapper.Reset();
            Mapper.Initialize(cfg => { cfg.AddProfile<MapProfile>(); });
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);
            var date = DateTime.Now.AddDays(-40).ToString();

            //Act
            var result = controller.Get(date);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }


        [Fact]
        public void AddRetrospectiveWithNullParameter()
        {
            //Arrange
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);

            //Act
            var result = controller.Add(null);

            //Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void AddRetrospectiveWithNullDate()
        {
            //Arrange
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);
            var payload = new RetrospectiveWithoutFeedback { 
              Name="Five Project",
              Summary="Create five new projects",
              Date = DateTime.Parse("01/01/0001"),
              Participants="Mike Jerry,Tom Cat,Fred FilStone"
            };


            //Act
            var result = controller.Add(payload);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddRetrospectiveWithNullParticipant()
        {
            //Arrange
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);
            var payload = new RetrospectiveWithoutFeedback
            {
                Name = "Five Project",
                Summary = "Create five new projects",
                Date = DateTime.Parse("01/01/0001"),
                Participants = string.Empty
            };

            //Act
            var result = controller.Add(payload);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void AddRetrospectiveWithNullName()
        {
            //Arrange
            var loggerFactory = new LoggerFactory();
            var service = new Mock<IRetrospectiveService>();
            var logger = loggerFactory.CreateLogger<RetrospectivesController>();
            var controller = new RetrospectivesController(service.Object, logger);
            var payload = new RetrospectiveWithoutFeedback
            {
                Name = string.Empty,
                Summary = "Create five new projects",
                Date = DateTime.Parse("01/01/0001"),
                Participants = string.Empty
            };
            //Act
            var result = controller.Add(payload);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
