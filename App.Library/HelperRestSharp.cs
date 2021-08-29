using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App.Library
{
    public class HelperRestSharp
    {        
        public static IRestResponse CallApi(string host, string api, Method method, object dataObject = null)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(host)
            };
            var request = new RestRequest(api, method) { RequestFormat = DataFormat.Json };            
            if (dataObject != null)
            {
                request.AddBody(dataObject);
            }
            var response = client.Execute(request);
            return response;
        }
    }
}
