using System;
using System.Collections.Generic;
using zhtv.Models;

namespace zhtv.ViewModels
{
    class SongViewModel : BaseViewModel
    {
        Dictionary<int, Song> songs;
        BaseViewModel viewModel;

        public Dictionary<int, Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        public BaseViewModel ViewModel
        {
            get { return viewModel ?? new BaseViewModel(); }
            set { viewModel = value; }
        }

        public SongViewModel()
        {
            var sheet = new SheetViewModel()
            {
                Id = "",
                Range = ""
            };

            if (sheet.Values.Equals(null) || sheet.Values.Count < 1) return;

            Songs = new Dictionary<int, Song>();

            foreach (var row in sheet.Values)
            {
                Songs.Add(Convert.ToInt32(row[0]), new Song
                {
                    Title = row[1].ToString(),
                    Artist = row[2].ToString(),
                    Duration = Convert.ToInt32(row[3]),
                    VideoId = row[4].ToString(),
                    AlbumId = row[5].ToString(),
                    ArtistId = row[6].ToString()
                });
            }
        }
    }
}
