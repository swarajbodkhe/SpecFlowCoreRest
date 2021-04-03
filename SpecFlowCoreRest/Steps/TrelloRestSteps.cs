using RestSharp;
using SpecFlowCoreRest.Common;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;
using SpecFlowCoreRest.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace SpecFlowCoreRest.Features
{
    [Binding]
    public class TrelloRestSteps
    {
        private RestUtils restUtils;
        private IRestRequest restRequest;
        private IRestResponse restResponse;
        private Boards boards;

        [Given(@"I create a new Rest Client")]
        public void GivenICreateANewRestClient()
        {
            restUtils = new RestUtils();
        }

        [When(@"I create a new GET Request to ""(.*)""")]
        public void WhenICreateANewGETRequestTo(string p0)
        {
            restRequest = restUtils.createGETRequest(p0);
        }

        [When(@"I send the rest request")]
        public void WhenISendTheRestRequest()
        {
            restResponse = restUtils.getRestResponse(restRequest);
            try
            {
                boards = JsonConvert.DeserializeObject<Boards>(restResponse.Content.ToString());
            }
            catch (Exception)
            {

                boards = null;
            }
        }

        [Then(@"I validate the response code to be ""(.*)""")]
        public void ThenIValidateTheResponseCodeToBe(string p0)
        {
            string code = restResponse.StatusCode.ToString();
            code.Should().Be(p0);
        }

        [Then(@"I validate the response details for boards to be as following:")]
        public void ThenIValidateTheResponseDetailsForBoardsToBeAsFollowing(Table table)
        {
            Dictionary<string, object> dict = CommonFunctions.getModelDataFromTable(table);
            boards.validateBoards(dict);
        }

        [Then(@"I store the Board Id")]
        public void ThenIStoreTheBoardId()
        {
            CommonFunctions.setGlobalValue("Board.ID", boards.id);
        }

        [When(@"I create a new PUT Request to ""(.*)"" with body details from ""(.*)""")]
        public void WhenICreateANewPUTRequestToWithFollowingDetails(string p0, string p1)
        {
            string path=Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"DataFiles\" + p1);
            //string jSon = JsonConvert.DeserializeObject<string>(File.ReadAllText(path));

            JObject o1 = JObject.Parse(File.ReadAllText(path));

            string json = string.Empty;
            using (StreamReader file
                     = File.OpenText(path))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                JObject o2 = (JObject)JToken.ReadFrom(reader);

                json = o2.ToString();
            }
            restRequest = restUtils.createPUTPOSTRequest(p0, Method.PUT, json);

        }
    }
}
