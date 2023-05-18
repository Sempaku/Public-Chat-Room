using PCR_Server.Model;
using PCR_Server.Modesl;

namespace PCR_Server.Controllers
{
    public class RoomController
    {
        public List<Room> Rooms { get; }

        public RoomController()
        {
            Rooms = new List<Room>();
        }

        public string CreateRoom(string roomName, string username, string connectionId)
        {
            var room = new Room(roomName);
            room.CurrentUsers.Add(new User(username, connectionId));
            Rooms.Add(room);
            Console.WriteLine($"Создана комната с " +
                $"\nID = {room.RoomId}" +
                $"\nName = {room.RoomName}" +
                $"\nSecretKey = {room.SecretKey}");
            return room.SecretKey;
        }

        public void JoinToRoom(string secretKey, string username, string connectionId)
        {
            Room? room = Rooms.FirstOrDefault(room => room.SecretKey == secretKey);
            room?.CurrentUsers.Add(new User(username, connectionId));
        }

        public bool CheckRoomName(string roomName)
        {
            // вернет true если уже есть такая комната
            return Rooms.Any(room => room.RoomName == roomName);
        }

        public bool CheckUsername(string username, string secretKey)
        {
            Room? room = GetRoomBySecretKey(secretKey);
            // вернет true если такое имя уже есть в комнате
            return room.CurrentUsers.Any(user => user.Username == username);
        }

        public Room? GetRoomBySecretKey(string? secretKey)
        {
            Room? room = Rooms.FirstOrDefault(room => room.SecretKey == secretKey);
            if (room != null)
                return room;
            return null;
        }

        public IEnumerable<string> GetClientsInRoomBySecretKey(string secretKey)
        {
            Room? room = GetRoomBySecretKey(secretKey)
                ?? throw new Exception("Room is null");
            return room.CurrentUsers.Select(user => user.ConnectionId);
        }

        public string? GetRoomNameBySecretKey(string? secretKey)
        {
            Room? room = Rooms.FirstOrDefault(room => room.SecretKey == secretKey)
                ?? throw new NullReferenceException("Room is null");
            string? roomName = room?.RoomName;
            return roomName;
        }
    }
}