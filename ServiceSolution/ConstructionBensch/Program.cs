using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
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
                // Enable metadata publishing.
                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                //smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                serviceHost.Description.Behaviors.Add(smb);
                serviceHost.Open();
                Console.WriteLine("Master I am here to serve you!");
                Console.WriteLine("Press any key to exit...");
                Console.Read();
                serviceHost.Close();
            }
        }
    }
}
