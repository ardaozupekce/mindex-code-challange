using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {

        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void GetReportingStructure_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";
            var expectedposition = "Development Manager";
            var expecteddepartment = "Engineering";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.IsNotNull(reportingStructure.employee.DirectReports);
            Assert.AreEqual(expectedFirstName, reportingStructure.employee.FirstName);
            Assert.AreEqual(expectedLastName, reportingStructure.employee.LastName);
            Assert.AreEqual(expectedposition, reportingStructure.employee.Position);
            Assert.AreEqual(expecteddepartment, reportingStructure.employee.Department);
            Assert.AreEqual(4, reportingStructure.numberOfReports);
            Assert.AreEqual(2, reportingStructure.employee.DirectReports.Count);
        }
    }
}
