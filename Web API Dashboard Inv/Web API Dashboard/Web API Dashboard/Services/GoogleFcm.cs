using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WEB_API_DASHBOARD.ViewModels;

namespace WEB_API_DASHBOARD.Services
{
    public class GoogleFcm
    {
        public void SendMessage(FCMViewModel fcm)
        {
            try
            {
                var result = "";
                var webAddr = "https://fcm.googleapis.com/fcm/send";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + fcm.server_key);
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(JsonConvert.SerializeObject(fcm.notificationInputDto));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

        public void SendToTopic(Message message)
        {


            try
            {
                var result = "";
                var webAddr = "https://fcm.googleapis.com/fcm/send";
                string server_key = "AAAALx-RgkQ:APA91bF5Si3LVxFedrQnTHieBrcX0Xm6jb5PJ2I0Ao831JNYIcnnmxirYSGPOL2ECt6yOJr0E96jbz3A0puLXyXkavMwoJHBb18xw5YDf9F8NBteleimeRgo8SJFW9OaL_Dk7zXQ9Azk";
                //string sender_id = "202393092676";

                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Headers.Add("Authorization:key=" + server_key);
                httpWebRequest.Method = "POST";


                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {

                    streamWriter.Write(JsonConvert.SerializeObject(message));
                    streamWriter.Flush();
                }

                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();

                }


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }
        }

    }
}