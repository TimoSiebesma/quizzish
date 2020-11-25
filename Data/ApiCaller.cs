using Newtonsoft.Json;
using Quizzish.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Quizzish.Data
{
    public class ApiCaller
    {
        private string uri => $"opentdb.com/api.php?amount=10&category={(int)Category}&difficulty={Difficulty.ToLower()}&type=multiple&encode=url3986";
        public Category Category { get; set; }
        public string Difficulty { get; set; }

        public ApiCaller(Category category, string difficulty)
        {
            Category = category;
            Difficulty = difficulty;
        }

        public IEnumerable<Result> Fetch()
        {
            using var httpClient = new HttpClient();
            var httpRespone = httpClient.GetAsync("https://" + uri).GetAwaiter().GetResult();
            var response = httpRespone.Content.ReadAsStringAsync().GetAwaiter().GetResult();

            var results = JsonConvert.DeserializeObject<Root>(response).results;

            results = results.Select(r => new Result
            {
                question = HttpUtility.UrlDecode(r.question),
                correct_answer = HttpUtility.UrlDecode(r.correct_answer),
                incorrect_answers = r.incorrect_answers.Select(ia => HttpUtility.UrlDecode(ia)),
            });

            return results; 
        }


        public class Result
        {
            public string category { get; set; }
            public string type { get; set; }
            public string difficulty { get; set; }
            public string question { get; set; }
            public string correct_answer { get; set; }
            public IEnumerable<string> incorrect_answers { get; set; }
        }

        public class Root
        {
            public int response_code { get; set; }
            public IEnumerable<Result> results { get; set; }
        }
    }
}
