using APILayer.Client.Contracts;
using APILayer.Entities.PersonServices;
using FluentAssertions;
using FluentAssertions.Execution;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace UserStories.SystemTests.Steps.PersonsSteps
{
    [Binding]
    public class BasicPersonServiceSteps : StepBase
    {
        private readonly IPersonRestService personsServiceRestApi;
        private RestResponse<BasicPeopleResponse> basicPeopleResponse;

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
        
        [Then(@"The response should be '(.*)'")]
        public void ThenTheResponseShouldBe(int expectedStatusCode)
        {
            var realStatusCodeUsersList = (int)this.basicPeopleResponse.StatusCode;
            realStatusCodeUsersList.Should().Be(expectedStatusCode, $"Real code {realStatusCodeUsersList} --- Expected code {expectedStatusCode}");
        }

        [Then(@"The people in the list should not be empty")]
        public void ThenThePeopleListShouldBeNotEmpty()
        {
            if (basicPeopleResponse.Data != null)
            {
                this.basicPeopleResponse.Data.Names.Should().NotBeEmpty();
            }
            else
            {
                this.basicPeopleResponse.Data.Should().NotBeNull($"Received people list is null, error: {this.basicPeopleResponse.ErrorMessage}, ex{this.basicPeopleResponse.ErrorException}");
            }
        }
    }
}
