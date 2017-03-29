using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AudaExplore.ApiGateway.Data.Models;
using AudaExplore.DispatchServices.Data.Models;
using Common.Logging;
using Newtonsoft.Json;

namespace AudaExplore.ApiGateway.Clients
{
	public class CapacityApiClient
	{
		private static ILog Logger = LogManager.GetLogger<CapacityApiClient>();

		public static IEnumerable<ApiGatewayResponse> GetApiGatewayResponses(ApiGatewayRequest apiGatewayRequest, string capacityUrl)
		{
			var apiGatewayResponses = new List<ApiGatewayResponse>();

			if (apiGatewayRequest == null)
			{
				Logger.Error("GetAppointmentDetails Receieved Null Request");
				return CreateErrorResponse(apiGatewayResponses, "ApiGatewayRequest must not be null.");
			}

			try
			{
				Logger.Debug("GetAppointmentDetails received request: " + JsonConvert.SerializeObject(apiGatewayRequest));
				var capacityRequest = InitializeCapacityRequest(apiGatewayRequest);

                Logger.Debug("About to call: JsonConvert.SerializeObject(capacityRequest);");
                var postBody = JsonConvert.SerializeObject(capacityRequest);
                Logger.Debug($"successfully called: SerializeObject(capacityRequest), resulting postBody: {postBody}");

                using (var client = new HttpClient())
				{
                    Logger.Debug($"About to call: client.PostAsync({capacityUrl}, ...)");
                    //Call GetCapacity WebAPI
                    using (var response = client.PostAsync(capacityUrl, new StringContent(postBody, Encoding.UTF8, "application/json")).Result)
					{
						if (response.StatusCode == HttpStatusCode.OK)
						{
                            
						    var result = response.Content.ReadAsStringAsync().Result;
                            Logger.Debug($"StatusCode == OK, result={result}");
                            Logger.Debug("About to Deserialize(result)...");
                            var capacityResponses =
								JsonConvert.DeserializeObject<IEnumerable<CapacityResponse>>(result);

							apiGatewayResponses.AddRange(capacityResponses.Select(cr => new ApiGatewayResponse()
							{
								CapacityAvailable = cr.CapacityAvailable,
								CapacityQuota = cr.CapacityQuota,
								DataKey = cr.DataKey,
								EndTime = cr.EndTime,
								HostName = Environment.MachineName,
								Login = apiGatewayRequest.Login,
								Resource = cr.Resource,
								ReturnCode = ServiceStatus.Success,
								StartTime = cr.StartTime,
								Message = "OK",
								TaskType = (TaskType)cr.TaskType,
								TimeStamp = DateTime.Now
							}));
						}
						else
						{
                            var result = response.Content.ReadAsStringAsync().Result;
                            Logger.Debug($"StatusCode != OK, result={result}");
                            Logger.Debug("About to Deserialize(result)...");
                            var error = JsonConvert.DeserializeObject<HttpError>(result);
                            Logger.Debug($"About to CreateErrorResponse with error {error.Message}");
                            return CreateErrorResponse(apiGatewayResponses, error.Message);
						}

					}
				}

			}
			catch (Exception ex)
			{
                Logger.Debug($"About to CreateErrorResponse with error {ex.Message}...");
                return CreateErrorResponse(apiGatewayResponses, $"GetAppointmentDetails threw this exception: {ex.Message}");
			}

			return apiGatewayResponses;

		}

		/// <summary>
		/// Transfers values from ApiGateway to WebApi object
		/// </summary>
		/// <param name="apiGatewayRequest">Incoming request object from client</param>
		/// <returns>CapacityRequest object for call to GetCapacity WebAPI</returns>
		private static CapacityRequest InitializeCapacityRequest(ApiGatewayRequest apiGatewayRequest)
		{
		    var capacityRequest = new CapacityRequest
		    {
		        ExternalOrgId = apiGatewayRequest.ExternalOrgId,
		        FromDate = apiGatewayRequest.FromDate,
		        PostalCode = apiGatewayRequest.PostalCode,
		        ToDate = apiGatewayRequest.ToDate,
		        TimeSlot = apiGatewayRequest.TimeSlot == null
		            ? (ServiceWindow?) null
		            : (ServiceWindow) apiGatewayRequest.TimeSlot,
		        TaskQueryType = new DispatchServices.Data.Models.TaskQueryType
		        {
		            Audit = apiGatewayRequest.TaskQueryType.Audit,
		            Catastrophe = apiGatewayRequest.TaskQueryType.Catastrophe,
		            DeskReview = apiGatewayRequest.TaskQueryType.DeskReview,
		            DriveIn = apiGatewayRequest.TaskQueryType.DriveIn,
		            Estimate = apiGatewayRequest.TaskQueryType.Estimate,
		            SalvageYard = apiGatewayRequest.TaskQueryType.SalvageYard,
		            Supplement = apiGatewayRequest.TaskQueryType.Supplement
		        }

		        //ServiceWindow = apiGatewayRequest.ServiceWindow, //See below
		        //TaskQueryType = apiGatewayRequest.TaskQueryType, //See below
		        //ServerURL = apiGatewayRequest.ServerURL,//Set in Web API
		        //UserCompany = apiGatewayRequest.UserCompany//Set in Web API
		    };


		    return capacityRequest;
		}

		/// <summary>
		/// Creates appropriate error for svc to return to client.
		/// </summary>
		/// <param name="apiGatewayResponses">Initialized outgoing list of responses to client</param>
		/// <param name="error">string error message</param>
		/// <returns>Response list with new error</returns>
		private static IEnumerable<ApiGatewayResponse> CreateErrorResponse(List<ApiGatewayResponse> apiGatewayResponses, string error)
		{
			apiGatewayResponses.Add(
				new ApiGatewayResponse
				{
					Message = $"An error occured: {error}",
					ReturnCode = ServiceStatus.Failed
				}
			);
			return apiGatewayResponses;
		}
	}

}

