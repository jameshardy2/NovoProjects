using System;
using System.Linq;
using AudaExplore.ApiGateway.Services;
using AudaExplore.ApiGateway.Data.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AudaExplore.ApiGateway.UnitTests
{
	[TestClass()]
	public class AppointmentServiceTests
	{
		[TestMethod()]
        [Ignore]
		public void GetAppointmentDetailsNullRequestTest()
		{
			var svc = new AppointmentService();

			var result = svc.GetAppointmentDetails(null).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("ApiGatewayRequest must not be null."));
		}

		[TestMethod()]
        public void GetAppointmentDetailsTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				FromDate = DateTime.Now.AddDays(4),
				ToDate = DateTime.Now.AddDays(5),//)DateTime.Now.AddDays(1),
				ExternalOrgId = "033W",//477O
				PostalCode = "92128",//92127
				TaskQueryType = new TaskQueryType() {Estimate = true},
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).ToList();

			foreach (var r in result)
			{
				Assert.AreEqual(r.ReturnCode, ServiceStatus.Success);
				Assert.IsTrue(r.CapacityAvailable != 0);
				Assert.IsTrue(r.CapacityQuota != 0);
				Assert.IsTrue(r.DataKey != null);
				Assert.IsTrue(r.HostName != null);
			}

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsBadPostalCodeTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "477O",
				PostalCode = "85000",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("Cannot resolve ExternalOrgId/PostalCode to an office"));

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsNoPostalCodeCannotResolveTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "477O",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("Cannot resolve ExternalOrgId/PostalCode to an office"));

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsBadExternalOrgIdTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "YYY",
				PostalCode = "92127",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("Could not find LDAP ID for the given region legacy id"));

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsZipOutOfRangeTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "XXX",//mexico
				PostalCode = "92127",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("No external office found"));

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsBadExternalOrgIdNoZipTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "YYY",
				//PostalCode = "92127",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("Could not find LDAP ID for the given region legacy id"));

		}

		[TestMethod()]
        [Ignore]
        public void GetAppointmentDetailsNoExtOrgIdTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				//ExternalOrgId = "033",
				TaskQueryType = new TaskQueryType() { Estimate = true },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("ExternalOrgId is required"));
		}

		[TestMethod()]
		[Ignore]
		public void GetAppointmentDetailsNoTaskQueryTypeTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "477O",
				//TaskQueryType = new string[] { "Estimate" },
				TimeSlot = TimeSlot.ALL
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("TaskQueryType is required"));
		}
		[TestMethod()]
		[Ignore]
		public void GetAppointmentDetailsNoTimeSlotTest()
		{
			var svc = new AppointmentService();

			var apiGatewayRequest = new ApiGatewayRequest
			{
				ExternalOrgId = "477O",
				TaskQueryType = new TaskQueryType() { Estimate = true }
				//TimeSlot = "ALL"
			};

			var result = svc.GetAppointmentDetails(apiGatewayRequest).First();

			Assert.AreEqual(result.ReturnCode, ServiceStatus.Failed);
			Assert.IsTrue(result.Message.Contains("TimeSlot is required"));
		}

		[TestMethod()]
		public void PingTest()
		{
			var svc = new AppointmentService();

			var result = svc.Ping();

			Assert.AreEqual(result, "OK");
		}
	}
}