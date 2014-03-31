using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Ut
{
    class Program
    {
        static void Main(string[] args)
        {

            using (var serviceHost = new ServiceHost(typeof(GeoDataService), new Uri[] {new Uri("http://localhost:5678/") })) 
            {
                
            }
            Console.WriteLine("Master I am here to serve you!");
            Console.WriteLine("Press any key to exit...");
            Console.Read();
        }
    }
}
