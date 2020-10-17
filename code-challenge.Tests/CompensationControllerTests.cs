using challenge.Data;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
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
        public void GetCompensationById_Returns_Ok()
        {
            // Arrange
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";

            // Execute
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{employeeId}");
            var response = getRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
        [TestMethod]
        public void CreateCompensation_Returns_CreatedAsync()
        {

            var employee = new Employee()
            {
                EmployeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f",
                DirectReports = new List<Employee>()
            };

            // Arrange
            var compensation = new Compensation()
            {
                CompensationId = 400,
                employee = employee,
                salary = 75000,
                effectiveDate = DateTime.Parse("11/01/2020"),
            };
            // Execute
            var requestContent = new JsonSerialization().ToJson(compensation);

            var postRequestTask = _httpClient.PostAsync("api/compensation",
               new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

            var newCompesnation = response.DeserializeContent<Compensation>();

            Assert.IsNotNull(newCompesnation.CompensationId);
            Assert.IsNotNull(compensation.employee);
            Assert.IsNotNull(compensation.employee.EmployeeId);
            Assert.AreEqual(compensation.salary, newCompesnation.salary);
            Assert.AreEqual(compensation.effectiveDate, newCompesnation.effectiveDate);
        }
    }
}
