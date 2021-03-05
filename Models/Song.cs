namespace zhtv.ViewModels
{
    class Song
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Artist { get; set; }
        public int Duration { get; set; }
        public string VideoID { get; set; }
        public string AlbumID { get; set; }
        public string ArtistID { get; set; }
        public System.Collections.Generic.Dictionary<string, string> OrderUser = new System.Collections.Generic.Dictionary<string, string>();
    }
}
