using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp;
using TechTalk.SpecFlow;
using RestSharp;



namespace PostCodeApiAutomation.StepDefinitions
{
    public class PostCodeAPISteps
    {
        private string _postCode;
        private RestSharp.RestResponse _response;
        private JObject _json;

        [Given(@"i have postcode ""(.*)""")]
        public void GivenIHavePostcode(string postcode)
        {
            _postCode = postcode;
        }

        [When(@"i call the postcode API")]
        public async Task WhenICallThePostcodeAPI()
        {
            var client = new RestClient("https://api.postcodes.io/");
            var request = new RestRequest($"postcodes/{_postCode}", Method.Get);
            _response = await client.ExecuteAsync(request);
            _json = JObject.Parse(_response.Content);
        }

        [Then(@"the response code should be (.*)")]
        public void ThenTheResponseCodeShouldBe(int expectedStatusCode)
        {
            Assert.That((int)_response.StatusCode, Is.EqualTo(expectedStatusCode));
        }

        [Then(@"the postcode should be ""(.*)""")]
        public void ThenThePostcodeShouldBe(string expectedPostCode)
        {
            string actualPostCode = _json["result"]?["postcode"]?.ToString();
            Assert.That(actualPostCode, Is.EqualTo(expectedPostCode));
        }

        [Then(@"the country should be ""(.*)""")]
        public void ThenTheCountryShouldBe(string expectedCounty)
        {
            string actualCountry = _json["result"]?["country"]?.ToString();
            Assert.That(actualCountry, Is.EqualTo(expectedCounty));
        }
    }

}