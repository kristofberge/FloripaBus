using System;
using NUnit.Framework;
using Moq;
using FloripaBus.ViewModel;
using FloripaBusService.Service;
using FloripaBusService.Exceptions;
using FloripaBus.Tests.Testables;
using FloripaBusService.Model;
using System.Collections.Generic;
using Moq.Protected;

namespace FloripaBus.Tests.ViewModel
{
    [TestFixture]
    public class ListViewModelTests
    {
        #region Setup
        private TestableListViewModel _viewModel;
        private Mock<IFloripaBusService> _service;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IFloripaBusService>();
            _viewModel = new TestableListViewModel(_service.Object);
        }
        #endregion

        #region Tests: Title
        [Test]
        public void ListViewModel_TitleSet()
        {
            //Assert
            _viewModel.Title = "Floripa Bus";
        }
        #endregion

        #region Tests: Message
        [Test]
        public void GetRoutesByStreet_SetsUnreachableMessage()
        {
            //Arrange
            _service.Setup(s => s.GetRoutesByStreet(It.IsAny<string>())).Throws(new ApiNotReachableException());

            //Act
            _viewModel.CallShowRoutesForStreet("street");

            //Assert
            Assert.AreEqual(_viewModel.Message, ListViewModel.Messages.UNREACHABLE);
        }

        [Test]
        public void GetRoutesByStreet_SetsNotFoundMessage()
        {
            //Arrange
            _service.Setup(s => s.GetRoutesByStreet(It.IsAny<string>())).Throws(new ItemNotFoundException());

            //Act
            _viewModel.CallShowRoutesForStreet("street");

            //Assert
            Assert.AreEqual(_viewModel.Message, ListViewModel.Messages.NOT_FOUND);
        }

        [Test]
        public void GetRoutesByStreet_SetsUnknownMessage()
        {
            //Arrange
            _service.Setup(s => s.GetRoutesByStreet(It.IsAny<string>())).Throws(new Exception());

            //Act
            _viewModel.CallShowRoutesForStreet("street");

            //Assert
            Assert.AreEqual(_viewModel.Message, ListViewModel.Messages.UNKNOWN);
        }

        [Test]
        public void GetRoutesByStreet_SetsDefaultMessage()
        {
            //Arrange
            _service.Setup(s => s.GetRoutesByStreet(It.IsAny<string>())).ReturnsAsync(GetRoutes(3));

            //Act
            _viewModel.CallShowRoutesForStreet("street");

            //Assert
            Assert.AreEqual(_viewModel.Message, ListViewModel.Messages.DEFAULT);
        }
        #endregion

        #region Tests: Data
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        public void GetRoutesByStreet_ShowsAllRoutes(int amount)
        {
            //Arrange
            var street = "street";
            List<Route> routes = GetRoutes(amount);
            _service.Setup(s => s.GetRoutesByStreet(street)).ReturnsAsync(routes);

            //Act
            _viewModel.CallShowRoutesForStreet(street);

            //Assert
            Assert.AreEqual(_viewModel.Routes.Count, amount);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void GetRoutesByStreet_RoutesDataCorrectly(int amount)
        {
            //Arrange
            var street = "street";
            List<Route> routes = GetRoutes(amount);
            _service.Setup(s => s.GetRoutesByStreet(street)).ReturnsAsync(routes);

            //Act
            _viewModel.CallShowRoutesForStreet(street);

            //Assert
            for (int i = 0; i < routes.Count; i++)
            {
                Assert.AreEqual(routes[i].LongName, _viewModel.Routes[i].LongName);
                Assert.AreEqual(routes[i].ShortName, _viewModel.Routes[i].ShortName);
            }
        }
        #endregion

        #region Helper methods
        private List<Route> GetRoutes(int amount)
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

        #endregion
    }
}
