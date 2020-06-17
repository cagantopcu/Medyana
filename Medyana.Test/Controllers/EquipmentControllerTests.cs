using Medyana.Api.Controllers;
using Medyana.Contract;
using Medyana.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Xunit;

namespace Medyana.Test.Controllers
{
    public class EquipmentControllerTests
    {
        EquipmentController _controller;
        IEquipmentRepositoryFake<Equipment> _repository;
        ILogger<EquipmentController> _logger;

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

            _repository = new EquipmentRepositoryFake();
            _controller = new EquipmentController(_repository, _logger);
        }

        [Fact]
        public void GetList_WhenCalled_ReturnsSameResult()
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
            var response = _controller.Get();
            // Assert

            Assert.Equal(EquipmentListDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public void GetList_WhenCalled_ReturnsError()
        {
            this.ApiResultEquipmentDummyList.IsSucceed = false;
            this.ApiResultEquipmentDummyList.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = _controller.Get();
            // Assert

            Assert.Equal(this.ApiResultEquipmentDummyList.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsSameResult()
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
            var response = _controller.Get(EquipmentDummyModel.Id);
            // Assert

            Assert.Equal(EquipmentDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsError()
        {
            this.ApiResultEquipmentDummyItem.IsSucceed = false;
            this.ApiResultEquipmentDummyItem.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = _controller.Get(1);
            // Assert

            Assert.Equal(this.ApiResultEquipmentDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Put_WhenCalled_ReturnsUpdatedResult()
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
            var response = _controller.Put(updateModel);

            Assert.Equal(EquipmentDummyModel.Name, response.Result.Name);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public void Put_WhenCalled_ReturnsError()
        {
            var updateModel = new Equipment() { Id = 1, Name = "Updated Equipment Name" };

            this.ApiResultEquipmentDummyItem.IsSucceed = false;
            this.ApiResultEquipmentDummyItem.ErrorMessage = "Record Could Not Found";

            BuildDummyData();
            var response = _controller.Put(updateModel);

            Assert.Equal(this.ApiResultEquipmentDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsTrue()
        {
            ApiResultEquipmentDeleteDummyModel.Result = true;
            this.ApiResultEquipmentDeleteDummyModel.IsSucceed = true;

            BuildDummyData();
            var response = _controller.Delete(1);

            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.Result, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public void Delete_WhenCalled_ReturnsError()
        {
            ApiResultEquipmentDeleteDummyModel.Result = false;
            this.ApiResultEquipmentDeleteDummyModel.IsSucceed = false;
            this.ApiResultEquipmentDeleteDummyModel.ErrorMessage = "Record Could Not Fould";

            BuildDummyData();
            var response = _controller.Delete(1);

            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.Result, response.Result);
            Assert.Equal(this.ApiResultEquipmentDeleteDummyModel.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public void Post_WhenCalled_ReturnsSameResult()
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
            var response = _controller.Post(addModel);

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
        private IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }
    }
}

