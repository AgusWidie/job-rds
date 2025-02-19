using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEB_API_DASHBOARD.ViewModels
{
    public class FCMViewModel
    {
        // Firebase Cloud Messaging

        public string server_key { get; set; }
        public string sender_id { get; set; }
        public NotificationInputDto notificationInputDto { get; set; }
    }

    public class NotificationInputDto
    {
        public string to { get; set; }
        public Notification notification { get; set; }
        public Data data { get; set; }
    }

    public class Notification
    {
        public string body { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string type { get; set; }
    }
    public class Data
    {
        public string key1 { get; set; }
        public string key2 { get; set; }
    }


    public class Message
    {
        public List<Data1> Data { get; set; }
        public string Topic { get; set; }
    }

    public class Data1
    {
        public string data { get; set; }
    }
}