
/*
 * Copyright ©, AudaExplore, USA. This is UNPUBLISHED
 * PROPRIETARY SOURCE CODE of AudaExplore, USA; the contents of this file
 * may not be disclosed to third parties, copied or duplicated in any form, in
 * whole or in part, without the prior written permission of AudaExplore. 
 * ALL RIGHTS RESERVED.
 */


using System.Runtime.Serialization;

namespace AudaExplore.ApiGateway.Data.Models
{
    #region ServiceStatus

    /// <summary>
    ///     Indicates overall status of the service contract operation
    /// </summary>
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public enum ServiceStatus
    {
        /// <summary>
        ///     Service contract completed with warning
        /// </summary>
        [EnumMember(Value = "Warning")]
        Warning,

        /// <summary>
        ///     Service contract completed successfully
        /// </summary>
        [EnumMember(Value = "Success")]
        Success,

        /// <summary>
        ///     Service contract failed
        /// </summary>
        [EnumMember(Value = "Failed")]
        Failed
    }

    #endregion ServiceStatus
}
