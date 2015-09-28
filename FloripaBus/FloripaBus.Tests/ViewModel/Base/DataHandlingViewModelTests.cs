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

namespace FloripaBus.Tests.ViewModel.Base
{
    [TestFixture]
    public class DataHandlingViewModelTests
    {
        #region Setup
        private TestableDataHandlingViewModel _viewModel;

        [SetUp]
        public void Setup()
        {
            _viewModel = new TestableDataHandlingViewModel();
        }
        #endregion

        #region Tests: DataLoadingMode
        [TestCase(true)]
        [TestCase(false)]
        public void SetDataLoadingMode_IsLoadingSetCorrectly(bool dataLoading)
        {
            //Act
            _viewModel.CallSetDataLoadingMode(dataLoading);

            //Assert
            Assert.AreEqual(dataLoading, _viewModel.IsLoading);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void SetDataLoadingMode_ShowDataSetCorrectly(bool dataLoading)
        {
            //Act
            _viewModel.CallSetDataLoadingMode(dataLoading);

            //Assert
            Assert.AreNotEqual(dataLoading, _viewModel.ShowData);
        }
        #endregion
    }
}
