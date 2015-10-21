using System;
using System.Text;
using System.Collections.Generic;
using NUnit.Framework;
using FloripaBusService.Tests.Testables;
using FloripaBusService.Service;
using Moq;
using Moq.Protected;
using System.Net.Http;
using FloripaBusService.Exceptions;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Threading;

namespace FloripaBusService.Tests.Service
{
    [TestFixture]
    public class ServiceConnectorTests
    {
        private TestableServiceConnector _connector;
        private HttpClient _client;
        private Mock<HttpMessageHandler> _handler;

        [SetUp]
        public void Setup()
        {
            _connector = new TestableServiceConnector();
            _handler = new Mock<HttpMessageHandler>();
            _client = new HttpClient(_handler.Object);
            _connector.ClientToUSe = _client;
        }

        [Test]
        public void CreateAuthHeader_ReturnsCorrectAuthenticationHeader()
        {
            //Arrange
            var username = "username";
            var password = "password";

            byte[] bytes = Encoding.UTF8.GetBytes(string.Format("{0}:{1}", username, password));
            var expected = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(bytes));

            //Act
            var result = _connector.CallCreateAuthHeader(username, password);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        [ExpectedException(typeof(ApiNotReachableException))]
        public async void GetPostResponse_ThrowsApiNotReachableException()
        {
            //Arrange
            var uri = "http://testuri.com/api";
            var message = "TestMessage";

            _handler.Protected().Setup("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>()).Throws(new AggregateException());

            //Act
            await _connector.GetPostResponse(uri, message);
        }

        [Test]
        public void GetPostResponse_ReturnsHttpResponseMessage()
        {
            //Arrange
            var uri = "http://testuri.com/api";
            var message = "TestMessage";

            var expected = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            var content = "This is the returned content";
            expected.Content = new StringContent(content);

            _handler.Protected().Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(expected);

            //Act
            var result = _connector.GetPostResponse(uri, message).Result;

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
