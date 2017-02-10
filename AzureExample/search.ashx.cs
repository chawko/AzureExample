using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace AzureExample
{
    /// <summary>
    /// Summary description for search
    /// </summary>
    public class search : IHttpHandler
    {
        private static SearchServiceClient serviceClient;
        private static ISearchIndexClient indexClient;
        public void ProcessRequest(HttpContext context)
        {
            var term = context.Request.QueryString["term"];

            var key = "34FF26DE3EDB6D194C5F93A87AF526A2";
            var serviceName = "dev-bakingmad";
            if (serviceClient == null)
            {
                serviceClient = new SearchServiceClient(serviceName, new SearchCredentials(key));
            }

            if (indexClient == null)
            {
                indexClient = serviceClient.Indexes.GetClient("example");
            }

            Microsoft.Azure.Search.Models.DocumentSuggestResult<SuggestItem> results = indexClient.Documents.Suggest<SuggestItem>(term, "suggest", new Microsoft.Azure.Search.Models.SuggestParameters() { UseFuzzyMatching = true });
           
            context.Response.ContentType = "application/json";
            context.Response.Write(JsonConvert.SerializeObject(results.Results.Select(r=>r.Text)));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        [DataContract]
        public class SuggestResponse
        {
            [DataMember(Name = "value")]
            public List<SuggestItem> Values;
        }

        [DataContract]
        public class SuggestItem
        {
            [DataMember]
            public string Key;

            [DataMember]
            public string Name;
        }
    }

   

}