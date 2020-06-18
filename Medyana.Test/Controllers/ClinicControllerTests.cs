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
    public class ClinicControllerTests
    {
        ClinicController _controller;
        IClinicRepositoryFake<Clinic> _repository;
        ILogger<ClinicController> _logger;

        private ApiResult<Clinic> ApiResultClinicDummyItem = new ApiResult<Clinic>();
        private ApiResult<List<Clinic>> ApiResultClinicDummyList = new ApiResult<List<Clinic>>();
        private Clinic ClinicDummyModel = new Clinic();
        private List<Clinic> ClinicListDummyModel = new List<Clinic>();
        private ApiResult<bool> ApiResultClinicDeleteDummyModel = new ApiResult<bool>();
        private bool ClinicDeleteDummyModel = true;

        public ClinicControllerTests()
        {
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = loggerFactory.CreateLogger<ClinicController>();


            _repository = new ClinicRepositoryFake();
            //_controller = new ClinicController(_repository, _logger);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetList_WhenCalled_ReturnsSameResultAsync()
        {
            this.ApiResultClinicDummyList.IsSucceed = true;
            this.ClinicListDummyModel = new List<Clinic>() { new Clinic() { Id = 1, Name = "Çağan Clinic 1" } };

            BuildDummyData();
            var response = await _controller.GetAsync();
            // Assert

            Assert.Equal(ClinicListDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task GetList_WhenCalled_ReturnsErrorAsync()
        {
            this.ApiResultClinicDummyList.IsSucceed = false;
            this.ApiResultClinicDummyList.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = await _controller.GetAsync();
            // Assert

            Assert.Equal(this.ApiResultClinicDummyList.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_WhenCalled_ReturnsSameResultAsync()
        {
            this.ApiResultClinicDummyItem.IsSucceed = true;
            this.ClinicDummyModel = new Clinic() { Id = 1, Name = "Çağan Clinic 1" };

            BuildDummyData();
            var response = await _controller.GetAsync(ClinicDummyModel.Id);
            // Assert

            Assert.Equal(ClinicDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Get_WhenCalled_ReturnsErrorAsync()
        {
            this.ApiResultClinicDummyItem.IsSucceed = false;
            this.ApiResultClinicDummyItem.ErrorMessage = "Error Message";

            BuildDummyData();
            var response = await _controller.GetAsync(1);
            // Assert

            Assert.Equal(this.ApiResultClinicDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Put_WhenCalled_ReturnsUpdatedResultAsync()
        {
            var updateModel = new Clinic() { Id = 1, Name = "Updated Clinic Name" };

            this.ApiResultClinicDummyItem.IsSucceed = true;
            this.ClinicDummyModel = new Clinic() { Id = 1, Name = "Çağan Clinic 1" };


            BuildDummyData();
            var response = await _controller.PutAsync(updateModel);

            Assert.Equal(ClinicDummyModel.Name, response.Result.Name);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Put_WhenCalled_ReturnsErrorAsync()
        {
            var updateModel = new Clinic() { Id = 1, Name = "Updated Clinic Name" };

            this.ApiResultClinicDummyItem.IsSucceed = false;
            this.ApiResultClinicDummyItem.ErrorMessage = "Record Could Not Found";
            this.ClinicDummyModel = new Clinic() { Id = 2, Name = "Çağan Clinic 1" };


            BuildDummyData();
            var response = await _controller.PutAsync(updateModel);

            Assert.Equal(this.ApiResultClinicDummyItem.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_WhenCalled_ReturnsTrueAsync()
        {
            ApiResultClinicDeleteDummyModel.Result = true;
            this.ApiResultClinicDeleteDummyModel.IsSucceed = true;

            BuildDummyData();
            var response = await _controller.DeleteAsync(1);

            Assert.Equal(this.ApiResultClinicDeleteDummyModel.Result, response.Result);
            Assert.True(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Delete_WhenCalled_ReturnsErrorAsync()
        {
            ApiResultClinicDeleteDummyModel.Result = false;
            this.ApiResultClinicDeleteDummyModel.IsSucceed = false;
            this.ApiResultClinicDeleteDummyModel.ErrorMessage = "Record Could Not Fould";

            BuildDummyData();
            var response = await _controller.DeleteAsync(1);

            Assert.Equal(this.ApiResultClinicDeleteDummyModel.Result, response.Result);
            Assert.Equal(this.ApiResultClinicDeleteDummyModel.ErrorMessage, response.ErrorMessage);
            Assert.False(response.IsSucceed);
        }

        [Fact]
        public async System.Threading.Tasks.Task Post_WhenCalled_ReturnsSameResultAsync()
        {
            var addModel = new Clinic() { Id = 1, Name = "Added Clinic Name" };

            this.ApiResultClinicDummyItem.IsSucceed = true;
            this.ClinicDummyModel = addModel;


            BuildDummyData();
            var response = await _controller.Post(addModel);

            Assert.Equal(ClinicDummyModel, response.Result);
            Assert.True(response.IsSucceed);
        }



        private void BuildDummyData()
        {
            _repository.ApiResultDummyItem = this.ApiResultClinicDummyItem;
            _repository.ApiResultDummyList = this.ApiResultClinicDummyList;
            _repository.DummyModel = this.ClinicDummyModel;
            _repository.ListDummyModel = this.ClinicListDummyModel;
            _repository.ApiResultDeleteDummyModel = this.ApiResultClinicDeleteDummyModel;
            _repository.DeleteDummyModel = this.ClinicDeleteDummyModel;

        }
    }
}
