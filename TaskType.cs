using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudaExplore.ApiGateway.Data.Models
{
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public enum TaskType
	{
		[EnumMember(Value = "None")]
		None = 0,
		[EnumMember(Value = "Audit")]
		Audit = 1,
		[EnumMember(Value = "DeskReview")]
		DeskReview = 2,
		[EnumMember(Value = "Estimate")]
		Estimate = 3,
		[EnumMember(Value = "Supplement")]
		Supplement = 4,
		[EnumMember(Value = "SalvageYard")]
		SalvageYard = 5
	}
}
