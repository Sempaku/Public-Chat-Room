namespace PCR_Client.Models
{
    public class Message
    {
        public string Username { get; set; }
        public string MessageText { get; set; }
        public DateTime Time { get; set; }

        public Message(string username, string text)
        {
            Username = username;
            MessageText = text;
            Time = DateTime.Now;
        }
    }
}