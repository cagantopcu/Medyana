using Medyana.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Medyana.Test.Models
{
    public class ClinicModelTests : ModelTestBase
    {
        private Clinic clinicModel = new Clinic()
        {
            Id = 1,
            Name = "Çağan Clinic 1",
        };

        [Fact]
        public void Clinic_EmptyName_ReturnsNameIsRequiredError()
        {
            clinicModel.Name = "";

            Assert.True(ValidateModel(clinicModel).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }

        [Fact]
        public void Clinic_Name_ReturnsNameIsValid()
        {
            Assert.False(ValidateModel(clinicModel).Any(v => v.MemberNames.Contains("Name") && v.ErrorMessage.Contains("required")));
        }
    }
}
