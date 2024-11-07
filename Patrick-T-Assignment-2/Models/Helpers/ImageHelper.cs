using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Patrick_T_Assignment_2.Models.Helpers
{
    public static class ImageHelper
    {
        public static IEnumerable<string> GetTags(string imageUrl)
        {
            string apiKey = "acc_96fc18d2b017bb2";
            string apiSecret = "99f14532335c0826369e27feba977e4d";

            string basicAuthValue = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(string.Format("{0}:{1}", apiKey, apiSecret)));

            var client = new RestClient("https://api.imagga.com/v2/tags");

            var request = new RestRequest();
            request.Method = Method.Get;
            request.AddParameter("image_url", imageUrl);
            request.AddHeader("Authorization", string.Format("Basic {0}", basicAuthValue));

            var response = client.Execute(request);

            string[] lines = response.Content.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            IEnumerable<string> results = lines.Where(l => l.Contains("\"tag\""));
            var result = new List<string>();

            int count = 0;

            foreach (var line in results)
            {
                // Limit to 3 tags
                if (count >= 3)
                    yield break;

                yield return line.Split(':')[2].Replace("}", string.Empty).Replace("\"", string.Empty).Replace("]", string.Empty);
                count++;
            }
        }
    }
}
