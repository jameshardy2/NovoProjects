/*
 * Copyright ©, AudaExplore, USA. This is UNPUBLISHED
 * PROPRIETARY SOURCE CODE of AudaExplore, USA; the contents of this file
 * may not be disclosed to third parties, copied or duplicated in any form, in
 * whole or in part, without the prior written permission of AudaExplore. 
 * ALL RIGHTS RESERVED.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Web;
using AudaExplore.ApiGateway.Clients;
using AudaExplore.ApiGateway.Data.Models;
using Common.Logging;
using Newtonsoft.Json;

namespace AudaExplore.ApiGateway.Services
{
    #region AppointmentService

    /// <summary>
    ///     Implementation of IAppointmentService
    /// </summary>  
    [ServiceBehavior(Namespace = "http://novo.audaexplore.com/apigateway")]
    public class AppointmentService : IAppointmentService
    {
        private static readonly ILog Logger = LogManager.GetLogger<AppointmentService>();
        private static readonly MessageHandler LoggerRequestResponse = new MessageHandler();

        #region GetAppointmentDetails

        /// <summary>
        ///     Client will call to retrieve time slots that are currently available for the requested date and service window
        /// </summary>
        /// <param name="apiGatewayRequest">
        ///     Instance of ApiGatewayRequest object
        /// </param>
        /// <returns>
        ///     Returns instance of ApiGatewayResponse object
        /// </returns>
        public IEnumerable<ApiGatewayResponse> GetAppointmentDetails(ApiGatewayRequest apiGatewayRequest)
        {
            var requestId = Guid.NewGuid().ToString();

            var url = WebOperationContext.Current == null || WebOperationContext.Current.IncomingRequest.UriTemplateMatch == null ? "GetAppointmentDetails" : WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            var method = WebOperationContext.Current == null ? "POST" : WebOperationContext.Current.IncomingRequest.Method;

            LoggerRequestResponse.LogRequestInfo(requestId, url, method, apiGatewayRequest);

            var response = CapacityApiClient.GetApiGatewayResponses(apiGatewayRequest, ConfigurationManager.AppSettings["capacityPath"]);

            LoggerRequestResponse.LogResponseInfo(requestId, url, method, null, null, response);

            return response;
        }

        #endregion GetAppointmentDetails


        #region Ping

        /// <summary>
        /// Client will call to verify connection
        /// </summary>
        /// <returns>OK</returns>
        public string Ping()
	    {
            var requestId = Guid.NewGuid().ToString();

            var url = WebOperationContext.Current == null || WebOperationContext.Current.IncomingRequest.UriTemplateMatch == null ? "Ping" : WebOperationContext.Current.IncomingRequest.UriTemplateMatch.RequestUri.AbsoluteUri;
            var method = WebOperationContext.Current == null ? "GET" : WebOperationContext.Current.IncomingRequest.Method;

            LoggerRequestResponse.LogRequestInfo(requestId, url, method, null);
            LoggerRequestResponse.LogResponseInfo(requestId, url, method, null, null, "OK");

            return "OK";
	    }

		#endregion

	}

	#endregion AppointmentService
}
