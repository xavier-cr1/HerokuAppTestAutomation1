using APILayer.Client.Contracts;
using APILayer.Entities;
using APILayer.Entities.PersonServices;
using FluentAssertions;
using FluentAssertions.Execution;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace UserStories.SystemTests.Steps.PersonsSteps
{
    [Binding]
    public class BasicPersonServiceSteps : StepBase
    {
        private readonly IPersonRestService personsServiceRestApi;
        private RestResponse<BasicPeopleResponse> basicPeopleResponse;
        private RestResponse restResponse;
        private People people;

        public BasicPersonServiceSteps(IPersonRestService personsServiceRestApi)
        {
            this.personsServiceRestApi = personsServiceRestApi;
            this.basicPeopleResponse = new RestResponse<BasicPeopleResponse>();
        }

        [Given(@"The user requests a list of people from express server")]
        public async Task GivenTheUserRequestsAListOfPeopleFromExpressServer()
        {
            this.basicPeopleResponse = await this.personsServiceRestApi.GetBasicPeopleResponse();
        }
        
        [Then(@"The rest basic people response should be '(.*)'")]
        public void ThenTheRestBasicPeopleResponseShouldBe(int expectedStatusCode)
        {
            var realStatusCodeUsersList = (int)this.basicPeopleResponse.StatusCode;
            realStatusCodeUsersList.Should().Be(expectedStatusCode, $"Real code {realStatusCodeUsersList} --- Expected code {expectedStatusCode}");
        }

        [Then(@"The rest response should be '(.*)'")]
        public void ThenTheRestResponseShouldBe(int expectedStatusCode)
        {
            var realStatusCodeUsersList = (int)this.restResponse.StatusCode;
            realStatusCodeUsersList.Should().Be(expectedStatusCode, $"Real code {realStatusCodeUsersList} --- Expected code {expectedStatusCode}");
        }

        [Given(@"The user posts a new person with the following properties")]
        [Then(@"The user posts a new person with the following properties")]
        public async Task ThenTheUserPostANewPersonWithProperties(Table table)
        {
            this.people = table.CreateInstance<People>();
            this.restResponse = await this.personsServiceRestApi.PostBasicPeopleResponse(this.people);
        }

        [Then(@"The people in the list should not be empty")]
        public void ThenThePeopleListShouldBeNotEmpty()
        {
            if(basicPeopleResponse.Content == null || string.IsNullOrEmpty(basicPeopleResponse.Content))
            {
                this.basicPeopleResponse.Content.Should().NotBeNull($"Received people list is null, error: {this.basicPeopleResponse.ErrorMessage}, ex{this.basicPeopleResponse.ErrorException}");
            }
            else
            {
                // TODO include deserialize in service?
                var basicPeopleList = JsonConvert.DeserializeObject<BasicPeopleResponse>(this.basicPeopleResponse.Content);

                basicPeopleList.People.Should().NotBeEmpty();
            }
        }
    }
}
