using System;
using System.Text;
using NUnit.Framework;
using FloripaBusService.Tests.Testables;
using FloripaBusService.Service;
using Moq;
using System.Net.Http;
using FloripaBusService.Exceptions;
using System.Threading.Tasks;

namespace FloripaBusService.Tests.Service
{
    [TestFixture]
    public class ResponseHandlerTests
    {
        TestableResponseHandler _client;
        Mock<IJsonCreator> _creator;
        Mock<IServiceConnector> _connector;

        [SetUp]
        public void Setup()
        {
            _creator = new Mock<IJsonCreator>();
            _connector = new Mock<IServiceConnector>();
            _client = new TestableResponseHandler(_creator.Object, _connector.Object);
        }

        #region Helper methods

        #region VaildateResponse
        [TestCase(System.Net.HttpStatusCode.GatewayTimeout)]
        [TestCase(System.Net.HttpStatusCode.RequestTimeout)]
        [ExpectedException(typeof(ApiNotReachableException))]
        public void ValidateResponse_ThrowsApiNotReachableException(System.Net.HttpStatusCode status)
        {
            //Arrange
            var response = new HttpResponseMessage();
            response.StatusCode = status;

            //Act
            _client.CallValidate(response);
        }

        [Test]
        [ExpectedException(typeof(ItemNotFoundException))]
        public void ValidateResponse_ThrowsItemNotFoundException()
        {
            //Arrange
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.NotFound;

            //Act
            _client.CallValidate(response);
        }

        [TestCase(System.Net.HttpStatusCode.BadGateway)]
        [TestCase(System.Net.HttpStatusCode.BadRequest)]
        [TestCase(System.Net.HttpStatusCode.Forbidden)]
        [TestCase(System.Net.HttpStatusCode.InternalServerError)]
        [TestCase(System.Net.HttpStatusCode.Unauthorized)]
        [ExpectedException(typeof(HttpRequestException))]
        public void ValidateResponse_ThrowsHttpRequestException(System.Net.HttpStatusCode status)
        {
            //Arrange
            var response = new HttpResponseMessage();
            response.StatusCode = status;

            //Act
            _client.CallValidate(response);
        }

        [Test]
        public void ValidateResponse_ThrowsNoException()
        {
            //Arrange
            var response = new HttpResponseMessage();
            response.StatusCode = System.Net.HttpStatusCode.OK;

            //Act
            _client.CallValidate(response);

            //Assert
            //Test will fail if exception thrown.
        }
        #endregion

        [Test]
        public void GetContent_ReturnsContentOfResponse()
        {
            //Arrange
            var content = "This is the content.";
            var response = new HttpResponseMessage();
            response.Content = new StringContent(content);

            //Act
            var result = _client.CallGetContent(response);

            //Assert
            Assert.AreEqual(content, result);
        }

        [Test]
        public void CreateUri_ReturnsCorrectUri()
        {
            //Arrange
            var method = "TestMethod";
            var expected = string.Format("https://api.appglu.com/v1/queries/{0}/run", method);

            //Act
            var result = _client.CallCreateUri(method);

            //Assert
            Assert.AreEqual(expected, result);
        }

        #endregion

        #region Getting reponses
        [Test]
        public void GetRoutesByStreet_ReturnsResponseStringContent()
        {
            //Arrange
            var street = "street1";
            var body = "Body for street1";
            _creator.Setup(c => c.GetRoutesBody(street)).Returns(body);

            var responseContent = "This is the response content.";
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(responseContent);

            var uri = _client.CallCreateUri(ResponseHandler.Constants.FIND_ROUTES_BY_STREET);

            _connector.Setup(con => con.GetPostResponse(uri, body)).ReturnsAsync(response);

            //Act
            string returned = _client.GetRoutesByStreet(street).Result;

            //Assert
            Assert.AreEqual(responseContent, returned);
        }

        [Test]
        public void GetStreetsByRoute_ReturnsResponseStringContent()
        {
            //Arrange
            var routeId = "Id1";
            var body = "Body for route Id1";
            _creator.Setup(c => c.GetRouteDetailsBody(routeId)).Returns(body);

            var responseContent = "This is the response content.";
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(responseContent);

            var uri = _client.CallCreateUri(ResponseHandler.Constants.FIND_STREETS_BY_ROUTE);

            _connector.Setup(con => con.GetPostResponse(uri, body)).ReturnsAsync(response);

            //Act
            string returned = _client.GetStreetsByRoute(routeId).Result;

            //Assert
            Assert.AreEqual(responseContent, returned);
        }

        [Test]
        public void GetDeparturesByRoute_ReturnsResponseStringContent()
        {
            //Arrange
            var routeId = "Id1";
            var body = "Body for route Id1";
            _creator.Setup(c => c.GetRouteDetailsBody(routeId)).Returns(body);

            var responseContent = "This is the response content.";
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            response.Content = new StringContent(responseContent);

            var uri = _client.CallCreateUri(ResponseHandler.Constants.FIND_DEPARTURES_BY_ROUTE);

            _connector.Setup(con => con.GetPostResponse(uri, body)).ReturnsAsync(response);

            //Act
            string returned = _client.GetDeparturesByRoute(routeId).Result;

            //Assert
            Assert.AreEqual(responseContent, returned);
        }
        #endregion
    }
}