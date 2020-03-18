using System;
using System.Collections.Generic;

namespace poc.providers.api.Models
{
    public class RequestService
    {
        public string EndPoint { get; set; }
        public dynamic Request { get; set; }
        public string Scope { get; set; }
        public Dictionary<string, string> Options { get; set; }

        public RequestService(string endPoint, dynamic request, string scope)
        {
            this.EndPoint = endPoint;
            this.Request = request;
            this.Scope = scope;
        }

        public RequestService(string endPoint, dynamic request, string scope, Dictionary<string, string> options)
        {
            this.EndPoint = endPoint;
            this.Request = request;
            this.Scope = scope;
            this.Options = options;
        }

        public RequestService()
        {
        }
    }
}
