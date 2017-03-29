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
    #region ApiGatewayRequest

    /// <summary>
    ///     AudaExplore generic api gateway request object.  This object has properties to hold credentials, message payload and any other additional information required
    ///     in order to perform workflow
    /// </summary>
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public class ApiGatewayRequest
    {
		[DataMember(IsRequired = true, EmitDefaultValue = false)]
		public string Login {get; set; }

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
		public string Password {get; set; }

        // Sarvesh MAY override from response to [Service #1] call
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
        public string ExternalOrgId { get; set; }

        [DataMember(IsRequired = false, EmitDefaultValue = false)]
        public string PostalCode { get; set; }

		// This call
        [DataMember(IsRequired = true, EmitDefaultValue = false)]
		public DateTime FromDate { get; set; }

        [DataMember(IsRequired = true, EmitDefaultValue = false)]
		public DateTime ToDate { get; set; }

		/*ALL, AM, PM */
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public TimeSlot? TimeSlot { get; set; }

		//Array of strings => (for now)
		/*public bool Estimate { get; set; }
        public bool Supplement { get; set; }
        public bool Audit { get; set; }
        public bool DeskReview { get; set; }
        public bool SalvageYard { get; set; }
        public bool Catastrophe { get; set; }
        public bool DriveIn { get; set; }*/
		[DataMember(IsRequired = false, EmitDefaultValue = false)]
		public TaskQueryType TaskQueryType { get; set; }

	}

	#endregion ApiGatewayRequest
}
