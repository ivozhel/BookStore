//using AutoMapper;
//using BookStore.AutoMapper;
//using BookStore.BL.Services;
//using BookStore.Controllers;
//using BookStore.DL.Interfaces;
//using BookStore.Models.Models;
//using BookStore.Models.Requests;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using Moq;

//namespace BookStore.Tests
//{
//    public class BookTest
//    {
//        private IList<Book> _books = new List<Book>()
//        {
//            new Book()
//            {
//                ID = 1,
//                Title ="Book1",
//                AuthorId = 1,
//                LastUpdated = DateTime.Now,
//                Quantity = 20,
//                Price = 10
//            },
//            new Book()
//            {
//                ID = 2,
//                Title ="Book2",
//                AuthorId = 2,
//                LastUpdated = DateTime.Now,
//                Quantity = 20,
//                Price = 10
//            },
//            new Book()
//            {
//                ID = 3,
//                Title ="Book3",
//                AuthorId = 3,
//                LastUpdated = DateTime.Now,
//                Quantity = 20,
//                Price = 10
//            },
//        };
//        private readonly IMapper _mapper;
//        private Mock<ILogger<BookService>> _logger;
//        private Mock<ILogger<BookController>> _loggerBookController;
//        private readonly Mock<IBookRepo> _bookRepoMock;
//        private readonly Mock<IAuthorRepo> _authorRepoMock;

//        public BookTest()
//        {
//            var mockMapperConfig = new MapperConfiguration(cfg =>
//            {
//                cfg.AddProfile(new AutoMappings());
//            });
//            _mapper = mockMapperConfig.CreateMapper();
//            _loggerBookController = new Mock<ILogger<BookController>>();
//            _bookRepoMock = new Mock<IBookRepo>();
//            _authorRepoMock = new Mock<IAuthorRepo>();
//        }

//        [Fact]
//        public async Task Book_GetAll_CountCheck()
//        {
//            //setup
//            var expectedCount = 3;
//            _bookRepoMock.Setup(x => x.GetAllBook()).ReturnsAsync(_books);

//            //inject
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Get();

//            //assert
//            var okObjectResult = result as OkObjectResult;
//            Assert.NotNull(okObjectResult);

//            var books = okObjectResult.Value as IEnumerable<Book>;
//            Assert.NotNull(books);
//            Assert.NotEmpty(books);
//            Assert.Equal(expectedCount, books.Count());
//        }

//        [Fact]
//        public async Task Book_GetByID()
//        {
//            //setup
//            var bookID = 1;
//            var book = _books.FirstOrDefault(x => x.ID == bookID);
//            _bookRepoMock.Setup(x => x.GetByID(bookID)).ReturnsAsync(book);

//            //inject
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Get(bookID);

//            //assert
//            var okObj = result as OkObjectResult;
//            Assert.NotNull(okObj);

//            var expBook = okObj.Value as Book;
//            Assert.NotNull(expBook);
//            Assert.Equal(bookID, expBook.ID);
//        }

//        [Fact]
//        public async Task Book_GetByID_NotFound()
//        {
//            //setup
//            var BookID = 4;
//            var Book = _books.FirstOrDefault(x => x.ID == BookID);
//            _bookRepoMock.Setup(x => x.GetByID(BookID)).ReturnsAsync(Book);

//            //inject
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Get(BookID);

//            //assert
//            var notFountObj = result as NotFoundObjectResult;
//            Assert.NotNull(notFountObj);

//            var returned = notFountObj.Value;
//            Assert.NotNull(returned);
//            Assert.Equal("Book with this id dose not exist", returned);

//        }

//        [Fact]
//        public async Task Book_AddBookOk()
//        {
//            //setup
//            var expectedID = 4;
//            var bookRequest = new BookRequest()
//            {
//                Title = "Book4",
//                AuthorId = 3,
//                Quantity = 20,
//                Price = 10
//            };
//            _authorRepoMock.Setup(x => x.GetByID(It.IsAny<int>())).ReturnsAsync(new Author());
//            _bookRepoMock.Setup(x => x.AddBook(It.IsAny<Book>())).Callback(() =>
//            {
//                var BookToReturn = new Book()
//                {
//                    ID = expectedID,
//                    Title = bookRequest.Title,
//                    AuthorId = bookRequest.AuthorId,
//                    Quantity = bookRequest.Quantity,
//                    Price = bookRequest.Price
//                };
//                _books.Add(BookToReturn);

//            })!.ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Add(bookRequest);

//            //assert
//            var OkObj = result as OkObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value as Book;
//            Assert.NotNull(response);
//            Assert.Equal(expectedID, response.ID);

//        }

//        [Fact]
//        public async Task Book_AddBook_AlreadyExists()
//        {
//            //setup
//            var bookRequest = new BookRequest();

//            _bookRepoMock.Setup(x => x.IsBookDuplicated(It.IsAny<BookRequest>())).ReturnsAsync(true);

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Add(bookRequest);

//            //assert
//            var OkObj = result as BadRequestObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value;
//            Assert.NotNull(response);
//            Assert.Equal("Book already exists", response);
//        }

//        [Fact]
//        public async Task Book_AddBook_AuthorDoseNotExist()
//        {
//            //setup
//            var bookRequest = new BookRequest();

//            _bookRepoMock.Setup(x => x.IsBookDuplicated(It.IsAny<BookRequest>())).ReturnsAsync(false);

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Add(bookRequest);

//            //assert
//            var OkObj = result as OkObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value;
//            Assert.Null(response);
//        }

//        [Fact]
//        public async Task Book_UpdateOk()
//        {
//            //setup
//            var expectedID = 3;
//            var bookRequest = new BookRequest()
//            {
//                Title = "Book3",
//                AuthorId = 3,
//                Quantity = 20,
//                Price = 10
//            };

//            _bookRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            _bookRepoMock.Setup(x => x.UpdateBook(It.IsAny<Book>())).Callback(() =>
//            {
//                _books.Where(x => x.ID == expectedID).FirstOrDefault().Title = bookRequest.Title;
//            })!.ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Update(bookRequest, expectedID);

//            //assert
//            var OkObj = result as OkObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value as Book;
//            Assert.NotNull(response);
//            Assert.Equal(bookRequest.Title, response.Title);

//        }

//        [Fact]
//        public async Task Book_Update_NotFound()
//        {
//            //setup
//            var expectedID = 4;

//            _bookRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Update(new BookRequest(), expectedID);

//            //assert
//            var OkObj = result as NotFoundObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value;
//            Assert.NotNull(response);
//            Assert.Equal("Book with this id dose not exist", response);

//        }

//        [Fact]
//        public async Task Book_DeleteOk()
//        {
//            //setup
//            var expectedID = 3;
//            var toDelete = _books.FirstOrDefault(x => x.ID == expectedID);
//            var beforeDeleteCount = _books.Count();

//            _bookRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            _bookRepoMock.Setup(x => x.DeleteBook(expectedID)).Callback(() =>
//            {
//                _books.Remove(toDelete);

//            })!.ReturnsAsync(() => toDelete);

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Delete(expectedID);

//            //assert
//            var OkObj = result as OkObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value as Book;
//            Assert.NotNull(response);
//            Assert.Equal(beforeDeleteCount - 1, _books.Count);

//        }

//        [Fact]
//        public async Task Book_Delete_NotFound()
//        {
//            //setup
//            var expectedID = 4;
//            var toDelete = _books.FirstOrDefault(x => x.ID == expectedID);
//            var beforeDeleteCount = _books.Count();

//            _bookRepoMock.Setup(x => x.GetByID(expectedID)).ReturnsAsync(() => _books.FirstOrDefault(x => x.ID == expectedID));

//            //infect 
//            var service = new BookService(_bookRepoMock.Object, _mapper, _authorRepoMock.Object);
//            var controller = new BookController(_loggerBookController.Object, service);

//            //act
//            var result = await controller.Delete(expectedID);

//            //assert
//            var OkObj = result as NotFoundObjectResult;
//            Assert.NotNull(OkObj);

//            var response = OkObj.Value;
//            Assert.NotNull(response);
//            Assert.Equal("Book with this id dose not exist", response);

//        }
//    }
//}
