using System;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using FloripaBusService.Service;
using FloripaBusService.Exceptions;
using FloripaBusService.Model;
using FloripaBusService;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FloripaBusService.Tests
{
    [TestFixture]
    public class FloripaBusServiceTests
    {
        #region Setup
        private Mock<ResponseHandler> _handler;
        private Mock<IJsonCreator> _creator;
        private Mock<IServiceConnector> _connector;
        private FloripaBusService.Service.FloripaBusService _service;

        [SetUp]
        public void Setup()
        {
            _creator = new Mock<IJsonCreator>();
            _connector = new Mock<IServiceConnector>();
            _handler = new Mock<ResponseHandler>(_creator.Object, _connector.Object);
            _service = new FloripaBusService.Service.FloripaBusService(_handler.Object);
        }
        #endregion

        #region GetRoutesByStreet

        [Test]
        public void GetRoutesByStreet_ReturnsListOfRoutes()
        {
            //Arrange
            int amount = 3;
            var routesToBeSent = GetListOfRoutes(amount);
            var objToBeSerialised = new { rows = routesToBeSent };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetRoutesByStreet(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            var returnedList = _service.GetRoutesByStreet(string.Empty).Result;

            //Assert
            for (int i = 0; i < amount; i++)
            {
                Assert.AreEqual(routesToBeSent[i].Id, returnedList[i].Id);
                Assert.AreEqual(routesToBeSent[i].AgencyId, returnedList[i].AgencyId);
                Assert.AreEqual(routesToBeSent[i].LastModifiedDate, returnedList[i].LastModifiedDate);
                Assert.AreEqual(routesToBeSent[i].LongName, returnedList[i].LongName);
                Assert.AreEqual(routesToBeSent[i].ShortName, returnedList[i].ShortName);
            }
        }

        [Test]
        [ExpectedException(typeof(ItemNotFoundException))]
        public async void GetRoutesByStreet_ThrowsItemNotFoundException()
        {
            //Arrange
            var emptyListOfRoutes = new List<Route>();
            var objToBeSerialised = new { rows = emptyListOfRoutes };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetRoutesByStreet(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            await _service.GetRoutesByStreet(string.Empty);
        }

        [Test]
        public async void GetRoutesByStreet_ThrowsItemNotFoundExceptionWithTypeStreet()
        {
            //Arrange
            var emptyListOfRoutes = new List<Route>();
            var objToBeSerialised = new { rows = emptyListOfRoutes };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetRoutesByStreet(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            try
            {
                await _service.GetRoutesByStreet(string.Empty);
            }
            catch (ItemNotFoundException e)
            {
                Assert.IsTrue(e.Type == ItemNotFoundException.ItemType.Street);
            }
        }

        #endregion

        #region GetStreetsByRoute

        [Test]
        public void GetStreetsByRoute_ReturnsListOfStreets()
        {
            //Arrange
            int amount = 3;
            var streetsToBeSent = GetListOfStreets(amount);
            var objToBeSerialised = new { rows = streetsToBeSent };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetStreetsByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            var returnedList = _service.GetStreetsByRoute(string.Empty).Result;

            //Assert
            for (int i = 0; i < amount; i++)
            {
                Assert.AreEqual(streetsToBeSent[i].Id, returnedList[i].Id);
                Assert.AreEqual(streetsToBeSent[i].Name, returnedList[i].Name);
                Assert.AreEqual(streetsToBeSent[i].Sequence, returnedList[i].Sequence);
            }
        }

        [Test]
        [ExpectedException(typeof(ItemNotFoundException))]
        public async void GetStreetsByRoute_ThrowsItemNotFoundException()
        {
            //Arrange
            var emptyListOfStreets = new List<Street>();
            var objToBeSerialised = new { rows = emptyListOfStreets };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetStreetsByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            await _service.GetStreetsByRoute(string.Empty);
        }

        [Test]
        public async void GetStreetsByRoute_ThrowsItemNotFoundExceptionWithTypeRoute()
        {
            //Arrange
            var emptyListOfStreets = new List<Street>();
            var objToBeSerialised = new { rows = emptyListOfStreets };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetStreetsByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            try
            {
                await _service.GetStreetsByRoute(string.Empty);
            }
            catch (ItemNotFoundException e)
            {
                Assert.IsTrue(e.Type == ItemNotFoundException.ItemType.Route);
            }
        }

        #endregion

        #region Tests: GetDeparturesByRoute

        [Test]
        public void GetDeparturesByRoute_ReturnsListOfDepartures()
        {
            //Arrange
            int amount = 3;
            var departuresToBeSent = GetListOfDepartures(amount);
            var objToBeSerialised = new { rows = departuresToBeSent };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetDeparturesByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            var returnedList = _service.GetDeparturesByRoute(string.Empty).Result;

            //Assert
            for (int i = 0; i < amount; i++)
            {
                Assert.AreEqual(departuresToBeSent[i].Id, returnedList[i].Id);
                Assert.AreEqual(departuresToBeSent[i].Calendar, returnedList[i].Calendar);
                Assert.AreEqual(departuresToBeSent[i].Time, returnedList[i].Time);
            }
        }

        [Test]
        [ExpectedException(typeof(ItemNotFoundException))]
        public async void GetDeparturesByRoute_ThrowsItemNotFoundException()
        {
            //Arrange
            var emptyListOfDepartures = new List<Street>();
            var objToBeSerialised = new { rows = emptyListOfDepartures };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetDeparturesByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            await _service.GetDeparturesByRoute(string.Empty);
        }

        [Test]
        public async void GetDeparturesByRoute_ThrowsItemNotFoundExceptionWithTypeRoute()
        {
            //Arrange
            var emptyListOfDepartures = new List<Street>();
            var objToBeSerialised = new { rows = emptyListOfDepartures };
            string json = JsonConvert.SerializeObject(objToBeSerialised);
            _handler.Setup(c => c.GetDeparturesByRoute(It.IsAny<string>())).ReturnsAsync(json);

            //Act
            try
            {
                await _service.GetDeparturesByRoute(string.Empty);
            }
            catch (ItemNotFoundException e)
            {
                Assert.IsTrue(e.Type == ItemNotFoundException.ItemType.Route);
            }
        }

        #endregion

        #region Helper methods

        private List<Route> GetListOfRoutes(int amount)
        {
            var routes = new List<Route>();
            for (int i = 0; i < amount; i++)
            {
                routes.Add(new Route
                {
                    Id = i.ToString(),
                    AgencyId = (10 + i).ToString(),
                    LastModifiedDate = DateTime.Now,
                    LongName = "LongName " + i,
                    ShortName = "Sh" + i
                });
            }
            return routes;
        }

        private List<Street> GetListOfStreets(int amount)
        {
            var streets = new List<Street>();
            for (int i = 0; i < amount; i++)
            {
                streets.Add(new Street
                {
                    Id = i.ToString(),
                    Name = "Street " + i,
                    Sequence = 10 + i
                });
            }
            return streets;
        }

        private List<Departure> GetListOfDepartures(int amount)
        {
            var departures = new List<Departure>();
            for (int i = 0; i < amount; i++)
            {
                departures.Add(new Departure
                {
                    Id = i.ToString(),
                    Calendar = "Calendar" + i,
                    Time = string.Format("{0}{0}:{0}{0}", i)
                });
            }
            return departures;
        }
        #endregion

    }
}
