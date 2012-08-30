using System;
using System.ComponentModel;
using System.Net;
using System.Web;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace NetDNARWS
{
    public class Api
    {
        private string _consumerKey = "";
        private string _consumerSecret = "";
        private string _alias = "";

        private const string _netDNABaseAddress = "https://rws.netdna.com";

        public Api(string alias, string consumerKey, string consumerSecret)
        {
            _consumerKey = consumerKey;
            _alias = alias;
            _consumerSecret = consumerSecret;
        }

        public dynamic Get(string url, bool debug = false)
        {            
            var requestUrl = GenerateOAuthRequestUrl(url, "GET");

            var request = new WebClient();
            var response = request.DownloadString(requestUrl);

            var result = JObject.Parse(response);

            if (debug)
                DumpObject(result);

            return result;
        }

        public bool Delete(string url)
        {
            var response = GetWebResponse(url, "DELETE");
            return ((HttpWebResponse) response).StatusCode == HttpStatusCode.OK;
        }

        private WebResponse GetWebResponse(string url, string method)
        {
            var requestUrl = GenerateOAuthRequestUrl(url, method);

            var request = WebRequest.Create(requestUrl);
            request.Method = method;

            var response = request.GetResponse();
            return response;
        }

        public bool Put(string url, dynamic data)
        {
            var requestUrl = GenerateOAuthRequestUrl(url, "PUT");

            var request = WebRequest.Create(requestUrl);
            request.Method = "PUT";

            var jsonData = JsonConvert.SerializeObject(data);
            byte[] byteArray = Encoding.UTF8.GetBytes(jsonData);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();

            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;            
        }

        private string GenerateOAuthRequestUrl(string url, string method)
        {
            Uri uri;
            Uri.TryCreate(_netDNABaseAddress + "/" + _alias + url, UriKind.Absolute, out uri);

            var normalizedUrl = "";
            var normalizedParams = "";

            var oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();
            var sig =
                HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, _consumerKey, _consumerSecret, "", "", method, timeStamp,
                                                              nonce, OAuthBase.SignatureTypes.HMACSHA1, out normalizedUrl,
                                                              out normalizedParams));
            var requestUrl = normalizedUrl + "?" + normalizedParams + "&oauth_signature=" + sig;
            return requestUrl;
        }

        private void DumpObject(dynamic o)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(o);
                Console.Write("{0}={1} ", name, value);
            }

            Console.WriteLine();
        }


    }
}
