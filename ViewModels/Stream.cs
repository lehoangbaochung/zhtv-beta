using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using zhtv.Models;

namespace zhtv.ViewModels
{
    class Stream
    {
        static readonly Dictionary<string, OrderInfo> OrderDictionary = new Dictionary<string, OrderInfo>();
        static readonly Random rd = new Random();
        static YouTubeService youtubeService;
        static string liveChatId = null;
        static int count = 0;

        private async Task Run()
        {
            UserCredential credential;
            using (var stream = new FileStream("client_secrets.json", FileMode.Open, FileAccess.Read))
            {
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                    new[] { YouTubeService.Scope.Youtube }, "user", CancellationToken.None, new FileDataStore(GetType().ToString()));
            }

            youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = GetType().ToString()
            });
        }

        private static LiveChatMessageListResponse MessageList(string videoId, string part)
        {
            if (youtubeService == null) new Stream().Run().Wait();

            if (liveChatId == null)
            {
                var videoListRequest = youtubeService.Videos.List("liveStreamingDetails");
                videoListRequest.Id = videoId;

                var videoListResponse = videoListRequest.Execute();
                liveChatId = videoListResponse.Items[0].LiveStreamingDetails.ActiveLiveChatId;
            }

            var liveChatMessageListRequest = youtubeService.LiveChatMessages.List(liveChatId, part);

            return liveChatMessageListRequest.Execute();
        }

        public static void Order(string videoId)
        {
            foreach (var item in MessageList(videoId, "snippet,authorDetails").Items)
            {
                if (int.TryParse(TextTrimming(item.Snippet.DisplayMessage), out int id) && !OrderDictionary.ContainsKey(item.Id)
                    && Player.Library.Find(s => s.ID == id) != null)
                {
                    OrderDictionary.Add(item.Id, new OrderInfo()
                    {
                        UserID = item.AuthorDetails.ChannelId,
                        UserName = item.AuthorDetails.DisplayName,
                        SongID = id
                    });
                }
            }

            if (OrderDictionary.Count > count)
            {
                for (int i = count; i < OrderDictionary.Count; i++) 
                    Player.OrderSong(OrderDictionary.ElementAt(i).Value);

                count = OrderDictionary.Count;
            }
        }

        private static string TextTrimming(string text)
        {
            var zm = new Regex("^ZM [1-9][0-9]*$");
            var zmt = new Regex("^ZMT .+$");

            string substr = null;

            if (zm.IsMatch(text)) substr = text.Substring(3);

            if (zmt.IsMatch(text))
            {
                foreach (var item in Player.Library)
                {
                    if (string.Compare(item.Name, text.Substring(4), true) == 0)
                        substr = item.ID.ToString();
                    else if (string.Compare(item.Name + " " + item.Artist, text.Substring(4), true) == 0)
                        substr = item.ID.ToString();
                }
            }

            return substr;
        }
    }
}
