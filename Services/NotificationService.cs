using CAPSTONEPROJECT.Settings;

using Microsoft.Extensions.Options;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CAPSTONEPROJECT.Services
{
    public class NotificationService
    {
        private readonly NotificationSettings _settings;

        public NotificationService(IOptions<NotificationSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task<bool> NotifyAsync(string to,string title,string body)
        {
            try
            {
                // Get the server key from FCM console
                var serverKey = string.Format("key={0}", _settings.AuthorizationKey);

                // Get the sender id from FCM console
                var senderId = string.Format("id={0}", _settings.SenderID);

                var data = new
                {
                    to, // Recipient device token
                    notification = new 
                    {
                        title, 
                        body 
                    }
                };

                // Using Newtonsoft.Json
                var jsonBody = JsonConvert.SerializeObject(data);

                using (var httpRequest = new HttpRequestMessage(HttpMethod.Post, "https://fcm.googleapis.com/fcm/send"))
                {
                    httpRequest.Headers.TryAddWithoutValidation("Authorization", serverKey);
                    httpRequest.Headers.TryAddWithoutValidation("Sender", senderId);
                    httpRequest.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    using (var httpClient = new HttpClient())
                    {
                        var result = await httpClient.SendAsync(httpRequest);

                        if (result.IsSuccessStatusCode)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return false;
        }
    }
}
