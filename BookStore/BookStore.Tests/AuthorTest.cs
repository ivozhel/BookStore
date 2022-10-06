using System.ComponentModel;
using AutoMapper;
using BookStore.AutoMapper;
using BookStore.BL.Services;
using BookStore.Controllers;
using BookStore.DL.Interfaces;
using BookStore.Models.Models;
using BookStore.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace BookStore.Tests
{
    public class AuthorTest
    {
        private IList<Author> _authors = new List<Author>()
        {
            new Author()
            {
                ID = 1,
                Name ="Author1",
                Nickname = "Nick1",
                DateOfBirth = DateTime.Now,
                Age = 20
            },
            new Author()
            {
                ID = 2,
                Name ="Author2",
                Nickname = "Nick2",
                DateOfBirth = DateTime.Now,
                Age = 22
            },
            new Author()
            {
                ID = 3,
                Name ="Author3",
                Nickname = "Nick3",
                DateOfBirth = DateTime.Now,
                Age = 20
            },
        };
        private readonly IMapper _mapper;
        private Mock<ILogger<AuthorService>> _logger;
        private Mock<ILogger<AuthorController>> _loggerAuthorController;
        private readonly Mock<IAuthorRepo> _authorRepoMock;
        private readonly Mock<IBookRepo> _bookRepoMock;

        public AuthorTest()
        {
            var mockMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMappings());
            });
            _mapper = mockMapperConfig.CreateMapper();
            _logger = new Mock<ILogger<AuthorService>>();
            _loggerAuthorController = new Mock<ILogger<AuthorController>>();
            _authorRepoMock = new Mock<IAuthorRepo>();
            _bookRepoMock = new Mock<IBookRepo>();
        }

        [Fact]
        public async Task Author_GetAll_CountCheck()
        {
            //setup
            var expectedCount = 3;
            _authorRepoMock.Setup(x => x.GetAll()).ReturnsAsync(_authors);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Get();

            //assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var authors = okObjectResult.Value as IEnumerable<Author>;
            Assert.NotNull(authors);
            Assert.NotEmpty(authors);
            Assert.Equal(expectedCount, authors.Count());
        }

        [Fact]
        public async Task Author_GetByID()
        {
            //setup
            var authorID = 1;
            var author = _authors.FirstOrDefault(x => x.ID == authorID);
            _authorRepoMock.Setup(x => x.GetByID(authorID)).ReturnsAsync(author);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Get(authorID);

            //assert
            var okObj = result as OkObjectResult;
            Assert.NotNull(okObj);

            var expAuthor = okObj.Value as Author;
            Assert.NotNull(expAuthor);
            Assert.Equal(authorID, expAuthor.ID);
        }

        [Fact]
        public async Task Author_GetByID_NotFound()
        {
            //setup
            var authorID = 4;
            var author = _authors.FirstOrDefault(x => x.ID == authorID);
            _authorRepoMock.Setup(x => x.GetByID(authorID)).ReturnsAsync(author);

            //inject
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Get(authorID);

            //assert
            var notFountObj = result as NotFoundObjectResult;
            Assert.NotNull(notFountObj);

            var returned = notFountObj.Value;
            Assert.NotNull(returned);
            Assert.Equal("Author with this id dose not exist", returned);

        }

        [Fact]
        public async Task Author_AddAuthorOk()
        {
            //setup
            var expectedID = 4;
            var authorRequest = new AuthorRequest()
            {
                Nickname = "Test Nick",
                Name = "TestAuthor",
                DateOfBirth = DateTime.Now,
                Age = 20
            };

            _authorRepoMock.Setup(x => x.AddAuthor(It.IsAny<Author>())).Callback(() =>
            {
                var authorToReturn = new Author()
                {
                    ID = expectedID,
                    Nickname = authorRequest.Nickname,
                    Name = authorRequest.Name,
                    DateOfBirth = authorRequest.DateOfBirth,
                    Age = authorRequest.Age
                };
                _authors.Add(authorToReturn);

            })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Add(authorRequest);

            //assert
            var OkObj = result as OkObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value as Author;
            Assert.NotNull(response);
            Assert.Equal(expectedID, response.ID);

        }

        [Fact]
        public async Task Author_AddAuthor_AlreadyExists()
        {
            //setup
            var authorRequest = new AuthorRequest();

            _authorRepoMock.Setup(x => x.GetAuthorByName(It.IsAny<string>())).ReturnsAsync(new Author());

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Add(authorRequest);

            //assert
            var OkObj = result as BadRequestObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value;
            Assert.NotNull(response);
            Assert.Equal("Author already exists", response);
        }

        [Fact]
        public async Task Author_UpdateOk()
        {
            //setup
            var expectedID = 3;
            var authorRequest = new AuthorRequest()
            {
                Nickname = "Test Nick",
                Name = "TestAuthor",
                DateOfBirth = DateTime.Now,
                Age = 20
            };

            _authorRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            _authorRepoMock.Setup(x => x.UpdateAuthor(It.IsAny<Author>())).Callback(() =>
            {
                _authors.Where(x => x.ID == expectedID).FirstOrDefault().Name = authorRequest.Name;

            })!.ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Update(authorRequest, expectedID);

            //assert
            var OkObj = result as OkObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value as Author;
            Assert.NotNull(response);
            Assert.Equal(authorRequest.Name, response.Name);

        }

        [Fact]
        public async Task Author_Update_NotFound()
        {
            //setup
            var expectedID = 4;
            var authorRequest = new AuthorRequest()
            {
                Nickname = "Test Nick",
                Name = "TestAuthor",
                DateOfBirth = DateTime.Now,
                Age = 20
            };

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Update(authorRequest, expectedID);

            //assert
            var OkObj = result as NotFoundObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value;
            Assert.NotNull(response);
            Assert.Equal("Author with this id dose not exist", response);

        }

        [Fact]
        public async Task Author_DeleteOk()
        {
            //setup
            var expectedID = 3;
            var toDelete = _authors.FirstOrDefault(x => x.ID == expectedID);
            var beforeDeleteCount = _authors.Count();

            _authorRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            _authorRepoMock.Setup(x => x.DeleteAuthor(expectedID)).Callback(() =>
            {
                _authors.Remove(toDelete);

            })!.ReturnsAsync(() => toDelete);

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Delete(expectedID);

            //assert
            var OkObj = result as OkObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value as Author;
            Assert.NotNull(response);
            Assert.Equal(beforeDeleteCount - 1, _authors.Count);

        }

        [Fact]
        public async Task Author_Delete_NotFound()
        {
            //setup
            var expectedID = 4;
            var toDelete = _authors.FirstOrDefault(x => x.ID == expectedID);
            var beforeDeleteCount = _authors.Count();

            _authorRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Delete(expectedID);

            //assert
            var OkObj = result as NotFoundObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value;
            Assert.NotNull(response);
            Assert.Equal("Author with this id dose not exist", response);

        }

        [Fact]
        public async Task Author_Delete_HaveBooks()
        {
            //setup
            var expectedID = 3;
            var toDelete = _authors.FirstOrDefault(x => x.ID == expectedID);

            _bookRepoMock.Setup(x => x.HaveBooks(expectedID)).ReturnsAsync(() => true);
            _authorRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _authors.FirstOrDefault(x => x.ID == expectedID));

            //infect 
            var service = new AuthorService(_authorRepoMock.Object, _mapper, _logger.Object, _bookRepoMock.Object);
            var controller = new AuthorController(_loggerAuthorController.Object, service);

            //act
            var result = await controller.Delete(expectedID);

            //assert
            var OkObj = result as OkObjectResult;
            Assert.NotNull(OkObj);

            var response = OkObj.Value;
            Assert.Null(response);

        }
    }
}