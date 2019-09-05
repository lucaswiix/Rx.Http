using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Rx.Http.Interceptors;
using Rx.Http.Requests;
using Rx.Http.Tests.Models;

namespace Rx.Http.Samples.Consumers
{
    public class TheMovieDatabaseConsumer : RxConsumer
    {
        public TheMovieDatabaseConsumer(HttpClient http): base(http, null)
        {
            http.BaseAddress = new Uri(@"https://api.themoviedb.org/3/");
        }
        public override void Setup(RxHttpRequestConventions conventions)
        {
            conventions.Interceptors.Add(new TheMovieDatabaseInterceptor());
            base.Setup(conventions);
        }

        public IObservable<Result> ListMovies() => Get<Result>("movie/popular");
    }

    public class TheMovieDatabaseInterceptor : RxInterceptor
    {
        public void Intercept(RxHttpRequest request)
        {
            request.QueryStrings.Add("api_key", "key");
        }
    }
}