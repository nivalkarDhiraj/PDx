using HCA.PatientDigital.Cache;
using HCA.PlatformDigital.Entity;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace HCA.PatientDigital.BL.Test
{
    public class LabReportManager_Test
    {
        //TODO: Need to complete the testing for this component

        List<LabReport> reports = null;
        
        [SetUp]
        public void Setup()
        {
            //patients = PopulatePatient();
            //Mock<IMemoryCache> _memCache = new Mock<IMemoryCache>();
            //Mock<IMemoryCacheProvider> _memoryCache = new Mock<IMemoryCacheProvider>();
        }
        [Test]
        public void LabReport_Create()
        {
            Mock<ILabReportManager> mock = new Mock<ILabReportManager>();
            LabReport labReport = new LabReport();
            var result = mock.Setup(p => p.Create(labReport)).Returns(labReport);            
            Assert.IsTrue(result!=null);
        }        
    }
}