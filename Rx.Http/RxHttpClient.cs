﻿using System;
using System.Net.Http;
using System.Threading.Tasks;
using Rx.Http.MediaTypes;
using Rx.Http.MediaTypes.Abstractions;
using Rx.Http.Requests;
using Rx.Http.Serializers;

namespace Rx.Http
{
    public class RxHttpClient
    {
        private HttpClient http;

        public RxHttpClient()
        {
            this.http = new HttpClient();
        }

        public RxHttpClient(HttpClient http)
        {
            this.http = http;
        }

        public IObservable<string> Get(string url)
        {
            return new RxGetHttpRequest(this.http, url).Request();
        }

        public IObservable<TResponse> Get<TResponse>(string url, Action<RxHttpRequestOptions> func = null) 
            where TResponse: class
        {
            return new RxGetHttpRequest(this.http, url, func)
                .Request<TResponse>();
        }
    }
}