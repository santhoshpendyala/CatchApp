using Newtonsoft.Json.Linq;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SendingPushNotificationusingParse.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            SendingPushNotification();
            //bool isSent=PushNotification("Hai Hello Vanakkam");
            return new string[] { "value1", "value2" };
           
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }

        async
        public void SendingPushNotification()

        {

            var parsePush = new ParsePush();
            parsePush.Channels = new List<string>() { "SampleChannel" };
            parsePush.Alert = "hi";
            
            await parsePush.SendAsync();

            //ParseInstallation installation = ParseInstallation.CurrentInstallation;
            //installation.AddUniqueToList("channels", "Giants");
            //installation.SaveAsync();
            //ParsePush.SubscribeAsync("Giants");

            //var push = new ParsePush();
            //push.Channels = new List<string> { "campaign" };
            //push.Alert = "The Giants just scored!";
            //push.Data
            //push.SendAsync();




            
            

            //var push = new ParsePush();
            //push.Channels = new List<string> { "SampleChannels" };
            //push.Alert = "The Giants just scored!";
            //push.SendAsync();

            ////var parsePush = new ParsePush();
            ////parsePush.Alert = "hi";
            //// parsePush.SendAsync();

            ////// sending to all with your own data
            ////parsePush.Data = new Dictionary<string, object>
            ////{
              
            ////    {"sound", "notify.caf"},// for ios
            ////    {"badge", "Increment"}// for ios notification count increment on icon
            ////};
            //// parsePush.SendAsync();


            //var push = new ParsePush();
            //push.Query = from installation in ParseInstallation.Query
            //             where installation.Get<bool>("scores") == true
            //             where installation.Channels.Contains("Giants")
            //             select installation;
            //push.Alert = "Giants scored against the A's! It's now 2-2.";
            // push.SendAsync();

            //ParseInstallation installation1 = ParseInstallation.CurrentInstallation;
            //installation1.AddUniqueToList("channels", "SampleChannel");
            //installation1.SaveAsync();
            //ParsePush.SubscribeAsync("SampleChannel");



         //   var androidPush = new ParsePush();
         //   androidPush.Data = new Dictionary<string, object>
         //   {
         //     {"alert", "Your suitcase has been filled with tiny robots!"}
              
         //   };

         ////   androidPush.Data = "Your suitcase has been filled with tiny robots!";
         //   androidPush.Query = from installation in ParseInstallation.Query
         //                       where installation.Channels.Contains("SampleChannel")
         //                       where installation.DeviceType == "android"
         //                       select installation;
         //    androidPush.SendAsync();
           
 

        }

        private bool PushNotification(string pushMessage)
        {
            bool isPushMessageSend = false;

            string postString = "";
            string urlpath = "https://api.parse.com/1/push";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(urlpath);
            postString = "{ \"channels\": [ \"Trials\"  ], " +
                             "\"data\" : {\"alert\":\"" + pushMessage + "\"}" +
                             "}";
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.ContentLength = postString.Length;
            httpWebRequest.Headers.Add("X-Parse-Application-Id", "ZLDomMFa6JeVOp6Z0aY7mJrXmKaGNxtWwDYpe3cb");
            httpWebRequest.Headers.Add("X-Parse-REST-API-KEY", "8PMizyitFyvrEHFaNLBOiSQ4sG796vW4M6F24N0H");
            httpWebRequest.Method = "POST";
            //httpWebRequest.UseDefaultCredentials = true;
            //httpWebRequest.Proxy = WebRequest.GetSystemWebProxy();
            //httpWebRequest.Proxy.Credentials = new NetworkCredential("sp91920", "13Dec2014", "techmahindra");
            httpWebRequest.Timeout = 10000;

            StreamWriter requestWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            requestWriter.Write(postString);
            requestWriter.Close();
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                JObject jObjRes = JObject.Parse(responseText);
                if (Convert.ToString(jObjRes).IndexOf("true") != -1)
                {
                    isPushMessageSend = true;
                }
            }

            return isPushMessageSend;
        }
    }
}