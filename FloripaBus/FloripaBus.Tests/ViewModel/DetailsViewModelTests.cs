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
    public class DetailsViewModelTests
    {
        #region Setup
        private DetailsViewModel _viewModel;
        private Route _route;

        [SetUp]
        public void Setup()
        {
            _route = new Route()
            {
                Id = "Id1",
                AgencyId = "Agency1",
                LastModifiedDate = new DateTime(),
                LongName = "LongName 1",
                ShortName = "Sh1"
            };
        }
        #endregion

        #region Tests: Title
        [Test]
        public void DetailsViewModel_TitleSet()
        {
            //Arrange
            var expected = string.Format("Details for {0}-{1}", _route.ShortName, _route.LongName);

            //Act
            _viewModel = new DetailsViewModel(_route);

            //Assert
            Assert.AreEqual(expected, _viewModel.Title);
        }
        #endregion
    }
}
