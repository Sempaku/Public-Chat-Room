using Microsoft.AspNetCore.SignalR;
using PCR_Server.Controllers;
using PCR_Server.Model;

namespace PCR_Server
{
    public class ChatHub : Hub
    {
        private static readonly RoomController RoomController = new();

        // Метод получает имя пользователя, сообщение и ключ комнаты с клиента
        // затем он добавляет сообщение в комнату на сервере
        // и отправляет сообщение всем подключенным к комнате пользователям на клиент
        public async Task SendMessage(string user, string message, string secretKey)
        {
            Room? room = RoomController.GetRoomBySecretKey(secretKey)
                ?? throw new Exception("Room is null");
            room.Messages.Add(new Message(user, message));

            await Clients.Clients(RoomController.GetClientsInRoomBySecretKey(secretKey))
                .SendAsync("ReceiveSendMessage", new object[] { new Message(user, message) });
        }

        // Метод получает ключ комнаты, затем находит по ключу список сообщений в комнате
        // и возвращает список на клиент
        public async Task GetHistory(string secretKey)
        {
            Room? room = RoomController.GetRoomBySecretKey(secretKey)
                ?? throw new Exception("Room is null");
            await Clients.Caller.SendCoreAsync("ReceiveGetHistory", new object[] { room.Messages });
        }

        // Метод получает имя комнаты и имя пользователя
        // через контроллер он создает комнату и добавляет в нее пользователя создавшего комнату
        // затем он возвращает ключ созданной комнаты на клиент
        public async Task CreateRoom(string roomName, string username)
        {
            string secretKey = RoomController.CreateRoom(roomName, username, Context.ConnectionId);
            await Clients.Caller.SendAsync("ReceiveCreateRoom", secretKey);
        }

        // Метод получает имя комнаты которую хотят создать, и проверяет
        // нет ли уже на сервере комнаты с таким же названием
        // затем он возвращает false если такой комнаты нет
        public async Task CheckRoomName(string roomName)
        {
            bool roomAlreadyExist = RoomController.CheckRoomName(roomName);
            await Clients.Caller.SendAsync("ReceiveCheckRoomName", roomAlreadyExist);
        }

        // Метод подключает полбзователя к команте по ключу
        // и ничего не возвращает
        public void JoinToRoom(string secretKey, string username)
        {
            RoomController.JoinToRoom(secretKey, username, Context.ConnectionId);
        }

        // Метод проверяет есть ли в комнате пользователь с таким именем,
        // если нет то вернет false
        public async Task CheckUsername(string username, string secretKey)
        {
            bool UsernameIsExist = RoomController.CheckUsername(username, secretKey);
            await Clients.Caller.SendAsync("ReceiveCheckUsername", UsernameIsExist);
        }

        public async Task GetRoomNameBySecretKey(string secretKey)
        {
            await Clients.Caller.SendAsync("ReceiveGetRoomNameBySecretKey",
                RoomController.GetRoomNameBySecretKey(secretKey));
        }

        public async Task Notify(string action, object[] obj)
        {
            if (action == "CreateRoom")
            {
                string text = $"Room is created [Secret Key] : {obj[0]}";
                await Clients.Client(Context.ConnectionId).SendAsync("ReceiveNotify", text);
            }
            else if (action == "JoinToRoom")
            {
                string? username = obj[0].ToString();
                string? secretKey = obj[1].ToString();
                if (username == null || secretKey == null)
                    throw new NullReferenceException("Username or Secret Key is null");

                string text = $"[{username}] entered to the room!";
                await Clients.Clients(RoomController.GetClientsInRoomBySecretKey(secretKey))
                    .SendAsync("ReceiveNotify", text);
            }
            else if (action == "LeaveFromRoom")
            {
                string? username = obj[0].ToString();
                string? secretKey = obj[1].ToString();
                if (username == null || secretKey == null)
                    throw new NullReferenceException("Username or Secret Key is null");

                string text = $"[{username}] leave from room ;(";
                await Clients.Clients(RoomController.GetClientsInRoomBySecretKey(secretKey))
                    .SendAsync("ReceiveNotify", text);
            }
            else
                throw new ArgumentException("Invalid arguments");
        }
    }
}