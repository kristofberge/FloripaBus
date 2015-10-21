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
    public class StreetsViewModelTests
    {
        #region Setup
        private TestableStreetsViewModel _viewModel;
        private Mock<IFloripaBusService> _service;
        private Route _route;

        [SetUp]
        public void Setup()
        {
            _service = new Mock<IFloripaBusService>();
            _route = new Route()
            {
                Id = "Id1",
                AgencyId = "Agency1",
                LastModifiedDate = new DateTime(),
                LongName = "LongName 1",
                ShortName = "Sh1"
            };
            _service.Setup(s => s.GetStreetsByRoute(_route.Id)).ReturnsAsync(GetStreets(0));
            _viewModel = new TestableStreetsViewModel(_route, _service.Object);
        }
        #endregion

        #region Tests: Title
        [Test]
        public void StreetsViewModel_TitleSet()
        {
            //Assert
            _viewModel.Title = "Streets";
        }
        #endregion

        #region Tests: FillStreets

        [Test]
        public void StreetsViewModel_CallsFillStreets()
        {
            //Arrange
            var viewModel = new Mock<TestableStreetsViewModel>(_route, _service.Object);
            viewModel.Protected().Setup("FillStreets", _route.Id).Verifiable();

            //Assert
            var instantiate = viewModel.Object;

            //Assert
            viewModel.Protected().Verify("FillStreets", Times.Once(), _route.Id);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        public void FillStreets_ShowsAllStreets(int amount)
        {
            //Arrange
            _service.Setup(s => s.GetStreetsByRoute(_route.Id)).ReturnsAsync(GetStreets(amount));

            //Act
            _viewModel.CallFillStreets(_route.Id);

            //Assert
            Assert.AreEqual(_viewModel.Streets.Count, amount);
        }

        [TestCase(1)]
        [TestCase(3)]
        public void FillStreets_ShowCorrectData(int amount)
        {
            //Arrange
            List<Street> streets = GetStreets(amount);
            _service.Setup(s => s.GetStreetsByRoute(_route.Id)).ReturnsAsync(streets);

            //Act
            _viewModel.CallFillStreets(_route.Id);

            //Assert
            for (int i = 0; i < streets.Count; i++)
            {
                Assert.AreEqual(streets[i].Id, _viewModel.Streets[i].Id);
                Assert.AreEqual(streets[i].Name, _viewModel.Streets[i].Name);
                Assert.AreEqual(streets[i].Sequence, _viewModel.Streets[i].Sequence);
            }
        }
        #endregion

        #region Tests: DataLoadingMode
        [Test]
        public void FillStreets_SetsAndUnsetsDataLoadingMode()
        {
            //Arrange
            var viewModel = new Mock<TestableStreetsViewModel>(_route, _service.Object) { CallBase = true };
            viewModel.Protected().Setup("SetDataLoadingMode", true).Verifiable();
            viewModel.Protected().Setup("SetDataLoadingMode", false).Verifiable();

            //Assert
            var instantiate = viewModel.Object;

            //Assert
            viewModel.Protected().Verify("SetDataLoadingMode", Times.Once(), true);
            viewModel.Protected().Verify("SetDataLoadingMode", Times.Once(), false);
        }
        #endregion

        #region Helper methods
        private List<Street> GetStreets(int amount)
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
        #endregion
    }
}
