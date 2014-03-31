using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Ut
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class GeoDataService 
        : IGeoDataService
    {

        string IGeoDataService.SayHello()
        {
            return "Hello";
        }
    }
}
