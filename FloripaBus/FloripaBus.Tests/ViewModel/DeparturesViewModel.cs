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
using System.Collections.ObjectModel;

namespace FloripaBus.Tests.ViewModel
{
    [TestFixture]
    public class DeparturesViewModel
    {
        #region Setup
        private TestableDeparturesViewModel _viewModel;
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
            _service.Setup(s => s.GetDeparturesByRoute(_route.Id)).ReturnsAsync(GetDepartures(0, "WEEKDAY"));
            _viewModel = new TestableDeparturesViewModel(_route, _service.Object);
        }
        #endregion

        #region Tests: Title
        [Test]
        public void DeparturesViewModel_TitleSet()
        {
            //Assert
            _viewModel.Title = "Departures";
        }
        #endregion

        #region Tests: FillDepartures

        [Test]
        public void DeparturesViewModel_CallsFillDepartures()
        {
            //Arrange
            var viewModel = new Mock<TestableDeparturesViewModel>(_route, _service.Object);
            viewModel.Protected().Setup("FillDepartures", _route.Id).Verifiable();

            //Assert
            var instantiate = viewModel.Object;

            //Assert
            viewModel.Protected().Verify("FillDepartures", Times.Once(), _route.Id);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        [TestCase(10)]
        public void FillDepartures_ShowsAllData(int amount)
        {
            //Arrange
            _service.Setup(s => s.GetDeparturesByRoute(_route.Id)).ReturnsAsync(GetDepartures(amount, "WEEKDAY"));

            //Act
            _viewModel.CallFillDepartures(_route.Id);

            //Assert
            Assert.AreEqual(_viewModel.Weekdays.Count, amount);
        }

        [TestCase(1, 2, 3)]
        [TestCase(3, 5, 7)]
        [TestCase(5, 2, 6)]
        public void FillDepartures_SplitsDeparturesCorrectly(int weekday, int saturday, int sunday)
        {
            var departures = new List<Departure>();
            departures.AddRange(GetDepartures(weekday, "WEEKDAY"));
            departures.AddRange(GetDepartures(saturday, "SATURDAY"));
            departures.AddRange(GetDepartures(sunday, "SUNDAY"));
            //Arrange
            _service.Setup(s => s.GetDeparturesByRoute(_route.Id)).ReturnsAsync(departures);

            //Act
            _viewModel.CallFillDepartures(_route.Id);

            //Assert
            Assert.AreEqual(_viewModel.Weekdays.Count, weekday);
            Assert.AreEqual(_viewModel.Saturdays.Count, saturday);
            Assert.AreEqual(_viewModel.Sundays.Count, sunday);
        }

        [TestCase(1, 1, 1)]
        [TestCase(2, 2, 2)]
        public void FillDepartures_ShowsDataCorrectly(int weekday, int saturday, int sunday)
        {
            var departures = new List<Departure>();
            departures.AddRange(GetDepartures(weekday, "WEEKDAY"));
            departures.AddRange(GetDepartures(saturday, "SATURDAY"));
            departures.AddRange(GetDepartures(sunday, "SUNDAY"));
            //Arrange
            _service.Setup(s => s.GetDeparturesByRoute(_route.Id)).ReturnsAsync(departures);

            //Act
            _viewModel.CallFillDepartures(_route.Id);

            //Assert
            var result = new List<Departure>(_viewModel.Weekdays);
            result.AddRange(new List<Departure>(_viewModel.Saturdays));
            result.AddRange(new List<Departure>(_viewModel.Sundays));
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(result[i].Id, departures[i].Id);
                Assert.AreEqual(result[i].Time, departures[i].Time);
                Assert.AreEqual(result[i].Calendar, departures[i].Calendar);
            }
        }
        #endregion

        #region Tests: DataLoadingMode
        [Test]
        public void FillDepartures_SetsAndUnsetsDataLoadingMode()
        {
            //Arrange
            var viewModel = new Mock<TestableDeparturesViewModel>(_route, _service.Object) { CallBase = true };
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
        private List<Departure> GetDepartures(int amount, string calendar)
        {
            var departures = new List<Departure>();
            for (int i = 0; i < amount; i++)
            {
                departures.Add(new Departure
                {
                    Id = i.ToString(),
                    Calendar = calendar,
                    Time = string.Format("{0}{0}:{0}{0}", i)
                });
            }
            return departures;
        }
        #endregion
    }
}
