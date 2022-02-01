using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace APILayer.Client.Base
{
    public class RestApiBaseClient
    {
        protected readonly IConfigurationRoot _configurationRoot;
        protected readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        private string BaseServiceBaseUrl => this._configurationRoot.GetSection("AppConfiguration")["baseAPIService"];

        protected RestClient _restClient;

        public RestApiBaseClient(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            this._configurationRoot = configurationRoot;
            this._specFlowOutputHelper = specFlowOutputHelper;
            this._restClient = new RestClient(BaseServiceBaseUrl);
        }

        protected async Task<RestResponse<T>> SendRestRequestAsync<T>(RestRequest restRequest)
            where T : new()
        {
            this._specFlowOutputHelper.WriteLine($"Sending async {restRequest.Method} request to url: {BaseServiceBaseUrl+restRequest.Resource}");
            
            var response = await this._restClient.ExecuteAsync<T>(restRequest);

            if(!response.IsSuccessful)
            {
                return new RestResponse<T>() { ErrorMessage = response.ErrorMessage, StatusCode = response.StatusCode };
            }
            else
            {
                return response;
            }
        }
    }
}
