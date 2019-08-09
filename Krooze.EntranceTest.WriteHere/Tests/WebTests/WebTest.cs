using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Helpers;

namespace Krooze.EntranceTest.WriteHere.Tests.WebTests
{
    public class WebTest
    {

        public JObject GetAllMovies()
        {
            //TODO: Consume the following API: https://swapi.co/documentation using only .NET standard libraries (do not import the helpers on this page)
            // -Return the films object      
            return JObject.Parse(GetData());

        }

        public string GetDirector()
        {
            //TODO: Consume the following API: https://swapi.co/documentation using only .NET standard libraries (do not import the helpers on this page)
            // -Return the name of person that directed the most star wars movies, based on the films object return
            dynamic data = JsonConvert.DeserializeObject(GetData());
            var films = data.results;
            Dictionary<string, int> directors = new Dictionary<string, int>();

            foreach (var film in films)
            {
                string director = film.director.ToString();
                if (directors.Any(f => f.Key == director))
                {
                    directors[director]++;
                }
                else
                {
                    directors.Add(director, 1);
                }

            }

            return directors.FirstOrDefault(f => f.Value == directors.Max(g => g.Value)).Key;
        }

        private string GetData()
        {
            HttpClient client = new HttpClient();
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://swapi.co/api/films/");
            return client.SendAsync(requestMessage).Result.Content.ReadAsStringAsync().Result;
        }
    }
}
