using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AudaExplore.ApiGateway.Data.Models
{
    [DataContract(Namespace = "http://novo.audaexplore.com/apigateway")]
    public class TaskQueryType
	{
        [DataMember]
        public bool Estimate { get; set; }
        [DataMember]
        public bool Supplement { get; set; }
        [DataMember]
        public bool Audit { get; set; }
        [DataMember]
        public bool DeskReview { get; set; }
        [DataMember]
        public bool SalvageYard { get; set; }
        [DataMember]
        public bool Catastrophe { get; set; }
        [DataMember]
        public bool DriveIn { get; set; }
	}
}
