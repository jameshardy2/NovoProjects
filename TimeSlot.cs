using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudaExplore.ApiGateway.Data.Models
{
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public enum TimeSlot
	{
        [EnumMember(Value = "ALL")]
        ALL,
        [EnumMember(Value = "AM")]
        AM,
        [EnumMember(Value = "PM")]
        PM
	}
}
