using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Medyana.Test.Models
{
    public class EquipmentModelTests : ModelTestBase
    {
        private Equipment equipmentModel = new Equipment()
        {
            Id = 1,
            Name = "Çağan Equipment 1",
            ClinicId = 1,
            Quantity = 1,
            SupplyDate = new DateTime(),
            UnitPrice = 2,
            UsageRate = 25
        };

        [Fact]
        public void Equipment_Name_ReturnsNameIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_ClinicId_ReturnsClinicIdIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("ClinicId") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_Quantity_ReturnsQuantityIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("Quantity") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_SupplyDate_ReturnsSupplyDateIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("SupplyDate") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_UnitPrice_ReturnsUnitPriceIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("UnitPrice") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_UsageRate_ReturnsUsageRateIsValid()
        {
            Assert.False(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("UsageRate") && v.ErrorMessage.Contains("required")));
        }
        
        [Fact]
        public void Equipment_EmptyName_ReturnsNameIsRequiredError()
        {
            equipmentModel.Name = "";
            Assert.True(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Equipment_EmptyClinicId_ReturnsClinicIdIsRequiredError()
        {
            equipmentModel.ClinicId = 0;
            Assert.True(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("ClinicId") && v.ErrorMessage.Contains("between")));
        }

        [Fact]
        public void Equipment_EmptyQuantity_ReturnsQuantityIsRequiredError()
        {
            equipmentModel.Quantity = 0;
            Assert.True(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("Quantity") && v.ErrorMessage.Contains("between")));
        }

        [Fact]
        public void Equipment_EmptyUnitPrice_ReturnsUnitPriceIsRequiredError()
        {
            equipmentModel.UnitPrice = 0;
            Assert.True(ValidateModel(equipmentModel).Any(v => v.MemberNames.Contains("UnitPrice") && v.ErrorMessage.Contains("greater")));
        }

    }
}
