/*
 * Copyright ©, AudaExplore, USA. This is UNPUBLISHED
 * PROPRIETARY SOURCE CODE of AudaExplore, USA; the contents of this file
 * may not be disclosed to third parties, copied or duplicated in any form, in
 * whole or in part, without the prior written permission of AudaExplore. 
 * ALL RIGHTS RESERVED.
 */

using System.Runtime.Serialization;
using System;
using System.Collections.Generic;

namespace AudaExplore.ApiGateway.Data.Models
{
    #region ApiGatewayResponse

    /// <summary>
    ///     AudaExplore generic api gateway response object.  This object responsible informing status of the service call, it has properties to hold service status returncode, return message, etc...
    /// </summary>
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public class ApiGatewayResponse
    {
        // Holds status of the service
        [DataMember]
        public ServiceStatus ReturnCode;

		// Holds login id
        [DataMember]
		public string Login { get; set; }

		// Holds the machine name
        [DataMember]
		public string HostName { get; set; }

		// Holds the time of the response (datetime.now)
        [DataMember]
		public DateTime TimeStamp { get; set; }

		// Holds array of message object either OK or error message
		// try/catch 
        [DataMember]
		public string Message { get; set; }

        [DataMember]
		public string DataKey { get; set; }

        [DataMember]
		public string Resource { get; set; }

		//None, Audit, Estimate...
        [DataMember]
		public TaskType TaskType { get; set; }

		[DataMember]
		public DateTime StartTime { get; set; }

		[DataMember]
		public DateTime EndTime { get; set; }

        [DataMember]
		public long CapacityQuota { get; set; }

        [DataMember]
		public long CapacityAvailable { get; set; }
	}

    #endregion ApiGatewayResponse
}
