using System;
using System.Text;
using NUnit.Framework;
using FloripaBusService.Service;

namespace FloripaBusService.Tests.Service
{
    [TestFixture]
    public class JsonCreatorTests
    {
        private JsonCreator _creator;

        [SetUp]
        public void Setup()
        {
            _creator = new JsonCreator();
        }

        [Test]
        public void GetRoutesBody_ReturnsCorrectJsonString()
        {
            //Arrange
            var street = "teststreet";
            var expected = "{\"params\":{\"stopName\":\"%" + street + "%\"}}";

            //Act
            string result = _creator.GetRoutesBody(street);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetRouteDetailsBody_ReturnsCorrectJsonString()
        {
            //Arrange
            var routeId = "Id1";
            var expected = "{\"params\":{\"routeId\":\"" + routeId + "\"}}";

            //Act
            string result = _creator.GetRouteDetailsBody(routeId);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
