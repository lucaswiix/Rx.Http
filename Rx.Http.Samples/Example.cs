﻿using Microsoft.Extensions.Logging;
using Rx.Http.MediaTypes;
using Rx.Http.Samples.Consumers;
using Rx.Http.Tests.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Rx.Http.Samples
{
    class Example
    {
        private TheMovieDatabaseConsumer tmdbConsumer;
        private ILogger<RxHttpClient> logger;
        public Example(ILogger<RxHttpClient> logger, TheMovieDatabaseConsumer tmdbConsumer)
        {
            this.logger = logger;
            this.tmdbConsumer = tmdbConsumer;
        }

        public async Task Execute()
        {
            while(true)
            {
                using (var httpClient = new RxHttpClient(new HttpClient(), logger))
                {
                    //Get the html code from the google home page
                    var response = await httpClient.Get("http://www.google.com");

                    //Asynchronously, get the json from jsonplaceholder and serialize it. 
                    httpClient.Get<List<Todo>>("https://jsonplaceholder.typicode.com/todos/", options => 
                    {
                        options.RequestMediaType = new JsonHttpMediaType();
                        options.AddHeader("Authorization", "Bearer <token>");
                        options.QueryStrings.Add("name", "John Doe");
                    }).Subscribe(itens =>
                    {
                        Console.WriteLine("Json request finished!");
                    });

                    var item = await tmdbConsumer.ListMovies();

                    var postWithId = await httpClient.Post<Identifiable>(@"https://jsonplaceholder.typicode.com/posts", new Post()
                    {
                        Title = "Foo",
                        Body = "Bar",
                        UserId = 3
                    });

                    var rsd = await httpClient.Post(@"https://postman-echo.com/post");
                }

                Thread.Sleep(1000);
            }
        }
    }
}
