using System.Collections.Generic;

namespace zhtv.Models
{
    class Song : Property
    {
        int id, duration;
        string title, artist;
        string videoId, albumId, artistId;
        Dictionary<string, string> user;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(nameof(Id)); }
        }

        public int Duration
        {
            get { return duration; }
            set { duration = value; OnPropertyChanged(nameof(Duration)); }
        }

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged(nameof(Title)); }
        }

        public string Artist
        {
            get { return artist; }
            set { artist = value; OnPropertyChanged(nameof(Artist)); }
        }

        public string VideoId
        {
            get { return videoId; }
            set { videoId = value; OnPropertyChanged(nameof(VideoId)); }
        }

        public string AlbumId
        {
            get { return albumId; }
            set { albumId = value; OnPropertyChanged(nameof(AlbumId)); }
        }

        public string ArtistId
        {
            get { return artistId; }
            set { artistId = value; OnPropertyChanged(nameof(ArtistId)); }
        }

        public override string ToString()
        {
            return $"{ Title } - { Artist }";
        }

        /// <summary>
        /// Return a link to album image or artist image of song
        /// </summary>
        /// <returns>The URL of image</returns>
        public string ImageUrl()
            => AlbumId.Equals(null) || AlbumId.Equals(string.Empty) ?
            "https://y.gtimg.cn/music/photo_new/T001R300x300M000" + ArtistId + ".jpg" :
            "https://y.gtimg.cn/music/photo_new/T002R300x300M000" + AlbumId + ".jpg";
    }
}
