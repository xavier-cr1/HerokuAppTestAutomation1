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
                this._specFlowOutputHelper.WriteLine($"Unsuccesful sending async {restRequest.Method} request to url: {BaseServiceBaseUrl + restRequest.Resource}." +
                    $"error: {response.ErrorMessage}, ex: {response.ErrorException}");
                return new RestResponse<T>() { StatusCode = response.StatusCode };
            }
            else
            {
                return response;
            }
        }

        protected async Task<RestResponse> SendRestRequestAsync<T>(RestRequest restRequest, T item)
        {
            this._specFlowOutputHelper.WriteLine($"Sending async {restRequest.Method} request to url: {BaseServiceBaseUrl + restRequest.Resource}, with item: {item}");

            var response = await this._restClient.ExecuteAsync(restRequest);

            if (!response.IsSuccessful)
            {
                this._specFlowOutputHelper.WriteLine($"Unsuccesful sending async {restRequest.Method} request to url: {BaseServiceBaseUrl + restRequest.Resource}." +
                    $"error: {response.ErrorMessage}, ex: {response.ErrorException}");
                return new RestResponse() { StatusCode = response.StatusCode };
            }
            else
            {
                return response;
            }
        }
    }
}
