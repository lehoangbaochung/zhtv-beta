namespace zhtv.Models
{
    class User : Property
    {
        string id, name, message, imageUrl;

        public string Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }

        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set { imageUrl = value; OnPropertyChanged("ImageUrl"); }
        }
    }
}
