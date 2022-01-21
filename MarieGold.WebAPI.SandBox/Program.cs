using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MarieGold.WebAPI.SandBox {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");
            var token = "e7da2aff-67d8-4142-bde3-1803452b6389";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:5001/api/post?token={token}") {
                Content = new ByteArrayContent(File.ReadAllBytes("horror.jpeg"))
            };
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

            var response = client.Send(request);

            Console.WriteLine(response.ToString());
            Console.WriteLine(response.RequestMessage?.ToString());
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    }
}