/*
 * Copyright ©, AudaExplore, USA. This is UNPUBLISHED
 * PROPRIETARY SOURCE CODE of AudaExplore, USA; the contents of this file
 * may not be disclosed to third parties, copied or duplicated in any form, in
 * whole or in part, without the prior written permission of AudaExplore. 
 * ALL RIGHTS RESERVED.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using AudaExplore.ApiGateway.Data.Models;

namespace AudaExplore.ApiGateway.Services
{
    #region IAppointmentService

    /// <summary>
    ///     IAppointmentService will provide a gateway for Insurance Appointment workflow.  This inclues getting appointment availability, setting appointment etc...  
    /// </summary>
    [ServiceContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public interface IAppointmentService
    {
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
        [OperationContract]
        IEnumerable<ApiGatewayResponse> GetAppointmentDetails(ApiGatewayRequest apiGatewayRequest);
		
		#endregion GetAppointmentDetails

		
		#region Ping

		/// <summary>
		/// Client will call to verify connection
		/// </summary>
		/// <returns>OK</returns>
		[OperationContract]
		string Ping();
		
		#endregion
	}

	#endregion IAppointmentService
}
