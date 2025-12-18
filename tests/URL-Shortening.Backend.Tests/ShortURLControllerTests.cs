// <copyright file="ShortURLControllerTests.cs" company="Cole Clark">
// Copyright (c) Cole Clark. Licensed under the MIT License.
// </copyright>

namespace URL_Shortening.Backend.Tests
{
    using AutoFixture;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.VisualBasic;
    using Moq;
    using NUnit.Framework.Constraints;
    using URL_Shortening_Service.Backend.Controllers;
    using URL_Shortening_Service.Backend.Data;
    using URL_Shortening_Service.Backend.DTOs.Requests;
    using URL_Shortening_Service.Backend.Exceptions;
    using URL_Shortening_Service.Backend.Models;

    /// <summary>
    /// Contains the unit tests for the ShortURLController.
    /// </summary>
    [TestFixture]
    public class ShortURLControllerTests
    {
        private Mock<IShortURLRepository> shortURLRepoMock;
        private Fixture fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="ShortURLControllerTests"/> class.
        /// </summary>
        public ShortURLControllerTests()
        {
            this.fixture = new Fixture();
            this.shortURLRepoMock = new Mock<IShortURLRepository>();
        }

        /****************************************************** POST TESTS ******************************************************/

        /// <summary>
        /// Tests whether Post returns 201 created status when it is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Post_ShortURL_ReturnOk()
        {
            var url = this.fixture.Create<Task<ShortURL>>();

            this.shortURLRepoMock.Setup(repo => repo.Post(It.IsAny<CreateShortURLRequest>())).Returns(url);

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Post(It.IsAny<CreateShortURLRequest>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Post_ShortURL_ReturnedOk.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(201));
            }
        }

        /// <summary>
        /// Tests whether Post returns 400 bad request when it is unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Post_ShortURL_ThrowException()
        {
            this.shortURLRepoMock.Setup(repo => repo.Post(It.IsAny<CreateShortURLRequest>())).Throws(new Exception());

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Post(It.IsAny<CreateShortURLRequest>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Post_ShortURL_ThrowException.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(400));
            }
        }

        /****************************************************** GET TESTS ******************************************************/

        /// <summary>
        /// Tests whether Get returns 200 OK when it is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Get_ShortURL_ReturnOk()
        {
            var url = this.fixture.Create<Task<ShortURL>>();
            string testShortCode = "abcdefghij";

            this.shortURLRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Returns(url);

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Get(testShortCode);
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Get_ShortURL_ReturnOk.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(200));
            }
        }

        /// <summary>
        /// Tests whether Get returns 400 Bad Request when it is unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Get_ShortURL_ThrowException()
        {
            this.shortURLRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Throws(new Exception());
            string testShortCode = "abcdefghij";

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Get(testShortCode);
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Get_ShortURL_ThrowException.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(400));
            }
        }

        /// <summary>
        /// Tests whether Get returns 404 Not Found when it is unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Get_ShortURL_ThrowNotFound()
        {
            this.shortURLRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Throws(new NotFoundException("Test Message"));
            string testShortCode = "abcdefghij";

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Get(testShortCode);
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Get_ShortURL_ThrowNotFound.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(404));
            }
        }

        /****************************************************** PUT TESTS ******************************************************/

        /// <summary>
        /// Tests whether Put returns 200 OK status when it is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Put_ShortURL_ReturnOk()
        {
            var url = this.fixture.Create<Task<ShortURL>>();

            this.shortURLRepoMock.Setup(repo => repo.Put(It.IsAny<EditShortURLRequest>())).Returns(url);

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Put(It.IsAny<EditShortURLRequest>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Put_ShortURL_ReturnedOk.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(200));
            }
        }

        /// <summary>
        /// Tests whether Put returns 400 Bad Request when it is unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Put_ShortURL_ThrowException()
        {
            this.shortURLRepoMock.Setup(repo => repo.Put(It.IsAny<EditShortURLRequest>())).Throws(new Exception());

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Put(It.IsAny<EditShortURLRequest>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Put_ShortURL_ThrowException.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(400));
            }
        }

        /// <summary>
        /// Tests whether Put returns 404 Not Found when it is unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Put_ShortURL_ThrowNotFound()
        {
            this.shortURLRepoMock.Setup(repo => repo.Put(It.IsAny<EditShortURLRequest>())).Throws(new NotFoundException("Test Message"));

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Put(It.IsAny<EditShortURLRequest>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Put_ShortURL_ThrowNotFound.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(404));
            }
        }

        /****************************************************** DELETE TESTS ******************************************************/

        /// <summary>
        /// Tests whether Delete returns 204 No Content when it is successful.
        /// </summary>
        /// <returns>A <see cref="Task"/>representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Delete_ShortURL_ReturnOk()
        {
            this.shortURLRepoMock.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true);

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Delete(It.IsAny<int>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Delete_ShortURL_ReturnOk.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(204));
            }
        }

        /// <summary>
        /// Tests whether Delete returns 400 Bad Request when unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Delete_ShortURL_ThrowException()
        {
            this.shortURLRepoMock.Setup(repo => repo.Delete(It.IsAny<int>())).Throws(new Exception());

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Delete(It.IsAny<int>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Delete_ShortURL_ThrowException.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(400));
            }
        }

        /// <summary>
        /// Tests whether Delete returns 404 Not Found when unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
        [Test]
        public async Task Delete_ShortURL_ThrowNotFound()
        {
            this.shortURLRepoMock.Setup(repo => repo.Delete(It.IsAny<int>())).Throws(new NotFoundException("Test Message"));

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.Delete(It.IsAny<int>());
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Delete_ShortURL_ThrowNotFound.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(404));
            }
        }

        /****************************************************** REDIRECT TESTS ******************************************************/

        /// <summary>
        /// Tests whether RedirectToUrl returns 400 bad request when unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task Redirect_ShortURL_ThrowException()
        {
            this.shortURLRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Throws(new Exception());
            string testShortCode = "abcdefghij";

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.RedirectToUrl(testShortCode);
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Redirect_ShortURL_ThrowException.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(400));
            }
        }

        /// <summary>
        /// Tests whether RedirectToUrl returns 404 Not Found when unsuccessful.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous unit test.</returns>
        [Test]
        public async Task Redirect_ShortURL_ThrowNotFound()
        {
            this.shortURLRepoMock.Setup(repo => repo.Get(It.IsAny<string>())).Throws(new NotFoundException("Test Message"));
            string testShortCode = "abcdefghij";

            ShortURLController controller = new ShortURLController(this.shortURLRepoMock.Object);

            var result = await controller.RedirectToUrl(testShortCode);
            var obj = result as ObjectResult;

            if (obj is null)
            {
                Assert.Fail("obj was null in Redirect_ShortURL_ThrowNotFound.");
            }
            else
            {
                Assert.That(obj.StatusCode, Is.EqualTo(404));
            }
        }
    }
}