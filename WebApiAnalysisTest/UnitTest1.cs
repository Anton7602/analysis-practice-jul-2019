using Xunit;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebApiAnalysis.Controllers;
using Newtonsoft.Json;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using WebApiAnalysis;
using WebApiAnalysis.Interfaces;
using WebApiAnalysis.Services;
using WebApiAnalysis.Models;

namespace WebApiAnalysisTest
{
    public class BaseControllerTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public BaseControllerTest()
        {
            _server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<Startup>()
                .ConfigureTestServices(services =>
                {
                    services.AddSingleton<IDataStorage, DataStorage>();
                    services.AddSingleton<WebApiAnalysis.Models.Settings>();
                    services.AddControllers().AddNewtonsoftJson();
                }));
            _client = _server.CreateClient();
        }

        [Fact]
        public async void ServerRunningTest()
        {
            using (var response = await _client.GetAsync("/api/quizresults/test"))
            {
                var responseString = await response.Content.ReadAsStringAsync();
                Assert.Equal(responseString, "asd");
            }
        }

        [Fact]
        public async void RequestTest()
        {
            var requestJson = "";
            var requestContent = new StringContent(requestJson, Encoding.Default, "application/json");

            using (var response = await _client.PostAsync("/api/quizresults", requestContent)){
                Assert.Equal(response.StatusCode, HttpStatusCode.BadRequest);
            }
        }
    }
}