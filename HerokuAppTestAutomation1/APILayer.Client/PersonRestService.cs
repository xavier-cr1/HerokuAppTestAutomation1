using APILayer.Client.Base;
using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.PersonServices;
using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Infrastructure;

namespace APILayer.Client
{
    public class PersonRestService : RestApiBaseClient, IPersonRestService
    {
        private readonly string peopleAttribute = "/people/";

        public PersonRestService(IConfigurationRoot configurationRoot, ISpecFlowOutputHelper specFlowOutputHelper)
            : base(configurationRoot, specFlowOutputHelper)
        {
        }

        public async Task<RestResponse<BasicPeopleResponse>> GetBasicPeopleResponse()
        {
            var request = new RestRequest(peopleAttribute, Method.Get);

            return await this.SendRestRequestAsync<BasicPeopleResponse>(request);
        }

        public async Task<RestResponse> PostBasicPeopleResponse(People people)
        {
            var request = new RestRequest(peopleAttribute, Method.Post);

            request.AddJsonBody(people);

            return await this.SendRestRequestAsync(request, people);
        }
    }
}
