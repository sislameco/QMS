using Models.AppSettings;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http.Json;

namespace Utils.SlackUtil
{
    public class SlackLogging
    {
        private readonly string _slackErrorLoggerEndPoint = AppSettings.Slack.EndPoint;
        private string _content;

        public SlackLogging(string title, string endPoint, string message)
        {
            _content = JsonConvert.SerializeObject(new Payload
            {
                Channel = "qsmart-v3-exceptions",
                Blocks = new List<Section>
                {
                    new Section
                    {
                        Type = "header",
                        Text = new TextSection
                        {
                            Type = "plain_text",
                            Text = title
                        }
                    },
                    new Section
                    {
                        Type = "section",
                        Text = new TextSection
                        {
                            Type = "plain_text",
                            Text = "Received error from following API Url."
                        }
                    },
                    new Section
                    {
                        Type = "section",
                        Text = new TextSection
                        {
                            Type = "plain_text",
                            Text = endPoint
                        }
                    },
                    new Section
                    {
                        Type = "section",
                        Text = new TextSection
                        {
                            Type = "plain_text",
                            Text = "Error Detail:"
                        }
                    },
                    new Section
                    {
                        Type = "section",
                        Text = new TextSection
                        {
                            Type = "plain_text",
                            Text = message
                        }
                    }
                }
            });
        }

        public void SendAsync()
        {
            using (var client = new WebClient())
            {
                var data = new NameValueCollection { ["payload"] = _content };
                client.UploadValues(_slackErrorLoggerEndPoint, "POST", data);
            }
        }
    }

    public class Payload
    {
        [JsonProperty("channel")]
        public string Channel { get; set; }

        [JsonProperty("blocks")]
        public List<Section> Blocks { get; set; }
    }

    public class Section
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public TextSection Text { get; set; }
    }

    public class TextSection
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
    }




    #region send notification to user

    public static class SendNotificationToUser
    {
        public static async Task Send(string msg)
        {
            string channel = "branch-merged";

            using (HttpClient client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(AppSettings.Slack.WebhookUrl,
                    new
                    {
                        channel = channel,
                        text = msg
                    });

                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }

        public static async Task SendDirectToUser(string msg, string slackUserId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(AppSettings.Slack.BaseUrl);
                string postMessageEndpoint = "chat.postMessage";
                client.DefaultRequestHeaders.Add("Authorization", AppSettings.Slack.BotToken);
                HttpResponseMessage response = await client.PostAsJsonAsync(postMessageEndpoint,
                    new
                    {
                        channel = slackUserId,
                        text = msg
                    });
                string responseBody = await response.Content.ReadAsStringAsync();
            }
        }
    }

    #endregion
}
