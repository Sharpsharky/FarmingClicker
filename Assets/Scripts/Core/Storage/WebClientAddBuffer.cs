namespace Core.Storage
{
    using System;
    using System.Net;

    public class WebClientAddBuffer : WebClient
    {
        private readonly long range;

        public WebClientAddBuffer(long range)
        {
            this.range = range;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = (HttpWebRequest) base.GetWebRequest(address);
            request?.AddRange(range);
            return request;
        }
    }
}