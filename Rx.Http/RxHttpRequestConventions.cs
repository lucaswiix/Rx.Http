using Rx.Http.Interceptors;
using System.Collections.Generic;

namespace Rx.Http
{
    public class RxHttpRequestConventions
    {
        public string BaseUrl { get; set; }
        public List<RxInterceptor> Interceptors { get; set; }

        public RxHttpRequestConventions()
        {
            this.Interceptors = new List<RxInterceptor>();
        }
    }
}