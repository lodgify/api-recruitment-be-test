using System;
using System.Net.Http;
using System.Threading.Tasks;
using ApiApplication.Constants;
using ApiApplication.Resources;
using Newtonsoft.Json;
using Quartz;

namespace ApiApplication.Jobs
{
    public class ImdbJob: IJob
    {
        private IImdbPageStatus imdb;

        public ImdbJob(IImdbPageStatus imdb)
        {
            this.imdb = imdb;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var http = new HttpClient();
            var response = await http.GetAsync($"https://imdb-api.com/API/Usage/{ImdbConstants.ImdbApiKey}");
            if (response.StatusCode != System.Net.HttpStatusCode.OK) {
                imdb.Status.LastCall = DateTime.Now.ToString();
                imdb.Status.Up = false;
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ImdbUsage>(content);
            imdb.Status.LastCall = DateTime.Now.ToString();
            imdb.Status.Up = result != null && string.IsNullOrEmpty(result.ErrorMessage);
            return;
        }
    }
}

