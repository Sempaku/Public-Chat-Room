namespace PCR_Client.Models
{
    public class Room
    {
        public int RoomId { get; set; }
        private static int _roomId = 0;
        public string RoomName { get; set; }
        public string SecretKey { get; set; }
        public List<Message> Messages { get; set; }
        public List<User> CurrentUsers { get; set; }

        public Room(string roomName)
        {
            RoomName = roomName;
            RoomId = _roomId++;
            SecretKey = Guid.NewGuid().ToString();
            Messages = new List<Message>();
            CurrentUsers = new List<User>();
        }
    }
}