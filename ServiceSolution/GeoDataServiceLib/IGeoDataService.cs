using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ut
{
    [ServiceContract]
    public interface IGeoDataService
    {
        [OperationContract]
        public string SayHello();
    }
}
