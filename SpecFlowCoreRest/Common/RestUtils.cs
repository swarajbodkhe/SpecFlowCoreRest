using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpecFlowCoreRest.Common
{
    class RestUtils
    {
        RestClient client;
        public enum MethodType
        {
            GET,
            PUT,
            POST
        }

        public RestUtils()
        {
            client = new RestClient(CommonFunctions.getAppSettingValues("ApplicationBaseURI"));
        } 

        public IRestRequest createGETRequest(string Resource)
        {            
           return new RestRequest(Resource, Method.GET)
                .AddQueryParameter("key", CommonFunctions.getAppSettingValues("key"))
                .AddQueryParameter("token", CommonFunctions.getAppSettingValues("token"));
        }

        public IRestRequest createPUTPOSTRequest(string Resource,Method method,string jsonObjectbody)
        {
            IRestRequest restRequest= new RestRequest(Resource, method);
            restRequest.AddQueryParameter("key", CommonFunctions.getAppSettingValues("key"))
                .AddQueryParameter("token", CommonFunctions.getAppSettingValues("token"));
            restRequest.AddParameter("application/json", jsonObjectbody, ParameterType.RequestBody);
            return restRequest;
        }

        public IRestResponse getRestResponse(IRestRequest request)
        {
            return client.Execute(request);
        }

    }
}
