using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Message = PCR_Client.Models.Message;

namespace PCR_Client
{
    public class ConnectionContext
    {
        #region props

        private static HubConnection connection = null!;
        public static string LocalDomain { get; } = "https://localhost:7123/chatH";
        public static string Url { get; } = $"http://msgrsempaku.somee.com/chatH";

        public static bool ResultExistRoomByName { get; private set; }
        public static bool UsernameExistInRoom { get; private set; }

        #endregion props

        #region ctor

        public ConnectionContext()
        {
            try
            {
                connection = new HubConnectionBuilder()
                    .WithUrl(Url)
                    .WithAutomaticReconnect()
                    .ConfigureLogging(logging =>
                    {
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .Build();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }

        #endregion ctor

        #region start/stop connection

        public static async Task StartAsync()
        {
            try
            {
                await connection.StartAsync();
                MessageBox.Show("Successful connection to the server!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }

        public static async Task StopAsync()
        {
            try
            {
                await connection.StopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
            }
        }

        #endregion start/stop connection

        #region Receives

        public static void ReceiveAll()
        {
            ReceiveSendMessage();
            ReceiveNotify();
            ReceiveCreateRoom();
            ReceiveGetHistory();
            ReceiveCheckRoomName();
            ReceiveGetRoomNameBySecretKey();
            ReceiveCheckUsername();
        }

        private static void ReceiveSendMessage()
        {
            connection!.On<object[]>("ReceiveSendMessage", (messageObj) =>
            {
                Message message = JsonConvert.DeserializeObject<Message>(messageObj[0].ToString());
                Form1.listBox.Invoke(new Action(() =>
                {
                    Form1.listBox.Items.Add($"{message.Username} <{message.Time}> : {message.MessageText}");
                }));
            });
        }

        private static void ReceiveNotify()
        {
            connection.On<string>("ReceiveNotify", (text) =>
            {
                Form1.listBox.Invoke(new Action(() =>
                {
                    Form1.listBox.Items.Add($"{text}");
                }));
            });
        }

        private static void ReceiveGetHistory()
        {
            connection.On<object[]>("ReceiveGetHistory", messages =>
            {
                foreach (var message in messages)
                {
                    string? msgJson = message.ToString();
                    Program.MessagesHistory.Add(JsonConvert.DeserializeObject<Message>(msgJson));
                }
                Form1.listBox.Invoke(new Action(() =>
                {
                    Form1.listBox.Items.Clear();
                    foreach (Message message in Program.MessagesHistory)
                    {
                        Form1.listBox.Items.Add($"{message.Username} <{message.Time}> : {message.MessageText}");
                    }
                }));
            });
        }

        private static void ReceiveCreateRoom()
        {
            connection.On<string>("ReceiveCreateRoom", (secretKey) =>
            {
                Program.CurrentSecretKey = secretKey;
                MessageBox.Show($"Комната создана, ваш Secret Key = {secretKey}" +
                    $"\nОн уже скопирован в ваш буфер обмена");
            });
        }

        private static void ReceiveCheckRoomName()
        {
            connection.On<bool>("ReceiveCheckRoomName", result =>
            {
                ResultExistRoomByName = result;
            });
        }

        private static void ReceiveGetRoomNameBySecretKey()
        {
            connection.On<string>("ReceiveGetRoomNameBySecretKey", (roomName) =>
            {
                Program.CurrentRoomname = roomName;
            });
        }

        private static void ReceiveCheckUsername()
        {
            connection.On<bool>("ReceiveCheckUsername", result =>
            {
                UsernameExistInRoom = result;
            });
        }

        #endregion Receives

        #region Invokes

        public static async Task InvokeGetHistory(string secretKey)
        {
            await connection.InvokeAsync("GetHistory", secretKey);
        }

        public static async Task InvokeSendMessage(string username, string message, string secretKey)
        {
            await connection.InvokeAsync("SendMessage", username, message, secretKey);
        }

        public static async Task InvokeNotify(string action, object?[] obj)
        {
            await connection.InvokeAsync("Notify", action, obj);
        }

        public static async Task InvokeCreateRoom(string roomName, string username)
        {
            await connection.InvokeAsync("CreateRoom", roomName, username);
        }

        public static async Task InvokeCheckRoomName(string roomName)
        {
            await connection.InvokeAsync("CheckRoomName", roomName);
        }

        public static async Task InvokeJoinToRoom(string secretKey, string username)
        {
            await connection.InvokeAsync("JoinToRoom", secretKey, username);
        }

        public static async Task InvokeCheckUsername(string username, string secretKey)
        {
            await connection.InvokeAsync("CheckUsername", username, secretKey);
        }

        public static async Task InvokeGetRoomNameBySecretKey(string secretKey)
        {
            await connection.InvokeAsync("GetRoomNameBySecretKey", secretKey);
        }

        #endregion Invokes
    }
}