using Medyana.Api.Controllers;
using Medyana.Contract;
using Medyana.Model;
using Medyana.ResourceManager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;


namespace Medyana.Test.Controllers
{
    public class EquipmentControllerTests
    {
        EquipmentController _controller;
        IEquipmentRepositoryFake<Equipment> _repository;
        ILogger<EquipmentController> _logger;
        IStringLocalizer<SharedResources> _localizer;

        private ApiResult<Equipment> ApiResultEquipmentDummyItem = new ApiResult<Equipment>();
        private ApiResult<List<Equipment>> ApiResultEquipmentDummyList = new ApiResult<List<Equipment>>();
        private Equipment EquipmentDummyModel = new Equipment();
        private List<Equipment> EquipmentListDummyModel = new List<Equipment>();
        private ApiResult<bool> ApiResultEquipmentDeleteDummyModel = new ApiResult<bool>();
        private bool EquipmentDeleteDummyModel = true;

        public EquipmentControllerTests()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<EquipmentController>();

            var localizationMock = new Mock<IStringLocalizer<SharedResources>>();
            _localizer = localizationMock.Object;

            _repository = new EquipmentRepositoryFake();
            _controller = new EquipmentController(_repository, _logger, _localizer);
        }

        [Fact]
        public async Task GetList_WhenCalled_ReturnsSameResultAsync()
        {
            this.ApiResultEquipmentDummyList.IsSucceed = true;
            this.EquipmentListDummyModel = new List<Equipment>() {
                new Equipment() {
                Id = 1,
                Name = "Çağan Equipment 1",
                ClinicId = 1,
                Quantity = 1,
                SupplyDate = new DateTime(),
                UnitPrice = 2,
                UsageRate = 25
            }
            };

            BuildDummyData();
            var response = await _controller.GetAsync();
            // Assert

            Assert.Equal(EquipmentListDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async Task GetList_WhenCalled_ReturnsErrorAsync()
        {
            this.ApiResultEquipmentDummyList.IsSucceed = false;
            this.ApiResultEquipmentDummyList.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = await _controller.GetAsync();
            // Assert

            Assert.Equal(this.ApiResultEquipmentDummyList.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsSameResultAsync()
        {
            this.ApiResultEquipmentDummyItem.IsSucceed = true;
            this.EquipmentDummyModel = new Equipment()
            {
                Id = 1,
                Name = "Çağan Equipment 1",
                ClinicId = 1,
                Quantity = 1,
                SupplyDate = new DateTime(),
                UnitPrice = 2,
                UsageRate = 25
            };

            BuildDummyData();
            var response = await _controller.GetAsync(EquipmentDummyModel.Id);
            // Assert

            Assert.Equal(EquipmentDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async Task Get_WhenCalled_ReturnsErrorAsync()
        {
            this.ApiResultEquipmentDummyItem.IsSucceed = false;
            this.ApiResultEquipmentDummyItem.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = await _controller.GetAsync(1);
            // Assert

            Assert.Equal(this.ApiResultEquipmentDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsUpdatedResultAsync()
        {
            var updateModel = new Equipment()
            {
                Id = 1,
                Name = "Updated Equipment Name",
                ClinicId = 1,
                Quantity = 1,
                SupplyDate = new DateTime(),
                UnitPrice = 2,
                UsageRate = 25
            };

            this.ApiResultEquipmentDummyItem.IsSucceed = true;
            this.EquipmentDummyModel = new Equipment()
            {
                Id = 1,
                Name = "Çağan Equipment 1",
                ClinicId = 1,
                Quantity = 1,
                SupplyDate = new DateTime(),
                UnitPrice = 2,
                UsageRate = 25
            };


            BuildDummyData();
            var response = await _controller.PutAsync(updateModel);

            Assert.Equal(EquipmentDummyModel.Name, response.Result.Name);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async Task Put_WhenCalled_ReturnsErrorAsync()
        {
            var updateModel = new Equipment() { Id = 1, Name = "Updated Equipment Name" };

            this.ApiResultEquipmentDummyItem.IsSucceed = false;
            this.ApiResultEquipmentDummyItem.ErrorMessage = "Record Could Not Found";

            BuildDummyData();
            var response = await _controller.PutAsync(updateModel);

            Assert.Equal(this.ApiResultEquipmentDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsTrueAsync()
        {
            ApiResultEquipmentDeleteDummyModel.Result = true;
            this.ApiResultEquipmentDeleteDummyModel.IsSucceed = true;

            BuildDummyData();
            var response = await _controller.DeleteAsync(1);

            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.Result, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async Task Delete_WhenCalled_ReturnsErrorAsync()
        {
            ApiResultEquipmentDeleteDummyModel.Result = false;
            this.ApiResultEquipmentDeleteDummyModel.IsSucceed = false;
            this.ApiResultEquipmentDeleteDummyModel.ErrorMessage = "Record Could Not Fould";

            BuildDummyData();
            var response = await _controller.DeleteAsync(1);

            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.Result, response.Result);
            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async Task Post_WhenCalled_ReturnsSameResultAsync()
        {
            var addModel = new Equipment()
            {
                Id = 1,
                Name = "Çağan Equipment 1",
                ClinicId = 1,
                Quantity = 1,
                SupplyDate = new DateTime(),
                UnitPrice = 2,
                UsageRate = 25
            };

            this.ApiResultEquipmentDummyItem.IsSucceed = true;
            this.EquipmentDummyModel = addModel;


            BuildDummyData();
            var response = await _controller.PostAsync(addModel);

            Assert.Equal(EquipmentDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        private void BuildDummyData()
        {
            _repository.ApiResultDummyItem = this.ApiResultEquipmentDummyItem;
            _repository.ApiResultDummyList = this.ApiResultEquipmentDummyList;
            _repository.DummyModel = this.EquipmentDummyModel;
            _repository.ListDummyModel = this.EquipmentListDummyModel;
            _repository.ApiResultDeleteDummyModel = this.ApiResultEquipmentDeleteDummyModel;
            _repository.DeleteDummyModel = this.EquipmentDeleteDummyModel;

        }
    }
}

