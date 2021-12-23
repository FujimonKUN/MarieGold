using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace MarieGold {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, "http://localhost/post");
            request.Content = new ByteArrayContent(File.ReadAllBytes("penguin.jpeg"));

            var response = client.Send(request);
        }
    }
}