namespace PCR_Client.Forms
{
    public partial class RoomForm : Form
    {
        // 2 состояния FormMode
        // 1 - CreateRoom
        // 2 - Join To Room
        public string FormMode { get; } = "";

        #region ctor

        public RoomForm(string mode)
        {
            InitializeComponent();
            FormMode = mode;

            if (mode == "CreateRoom")
            {
                //Text = "Создание комнаты";
                Apply_button.Text = "Создать";
                label2.Text = "Введите название комнаты:";
            }
            else if (mode == "JoinToRoom")
            {
                //Text = "Войти в комнату";
                Apply_button.Text = "Войти";
                label2.Text = "Введите Secret Key комнаты:";
                roomName_textBox.Visible = false;
                secretKey_textBox.Location = new Point(12, 74);
            }
        }

        #endregion ctor

        private async void Apply_button_Click(object sender, EventArgs e)
        {
            if (FormMode == "CreateRoom")
            {
                // считываем имя комнаты и имя пользователя
                var roomName = roomName_textBox.Text;
                var username = username_textBox.Text;

                // проверка roomname & username на пустоту
                if (ValidateOnEmptyAndWhiteSpaces(roomName, "Имя комнаты") ||
                    ValidateOnEmptyAndWhiteSpaces(username, "Имя пользователя"))
                    return;

                // проверка есть ли уже комнаты с таким названием
                if (await CheckRoomName(roomName))
                    return;

                // создает комнату и добавляет текущего пользователя в нее
                await ConnectionContext.InvokeCreateRoom(roomName, username);

                Thread clipboardThread = new(() => Clipboard.SetText(Program.CurrentSecretKey));
                clipboardThread.SetApartmentState(ApartmentState.STA);
                clipboardThread.Start();
                clipboardThread.Join();

                // вносит имя текущего пользователя в клиент
                Program.CurrentUsername = username;
                Program.CurrentRoomname = roomName;
                // меняет главную форму на Form1 и закрывает текущую форму
                Program.Context.MainForm = new Form1();
                Program.Context.MainForm.Show();
                await ConnectionContext.InvokeNotify("CreateRoom", new object[] { Program.CurrentSecretKey });
                Close();
            }
            else if (FormMode == "JoinToRoom")
            {
                var secretKey = secretKey_textBox.Text;
                var username = username_textBox.Text;

                if (ValidateOnEmptyAndWhiteSpaces(secretKey, "Ключ комнаты") ||
                    ValidateOnEmptyAndWhiteSpaces(username, "Имя пользователя"))
                    return;

                if (await CheckUsernameOnRoom(username, secretKey))
                {
                    return;
                }
                await ConnectionContext.InvokeGetRoomNameBySecretKey(secretKey);
                await ConnectionContext.InvokeJoinToRoom(secretKey, username);

                Program.CurrentSecretKey = secretKey;
                Program.CurrentUsername = username;
                Program.Context.MainForm = new Form1();
                Program.Context.MainForm.Show();
                await ConnectionContext.InvokeNotify("JoinToRoom", new object[] { username, Program.CurrentSecretKey });
                Close();
            }
            else
            {
                throw new ArgumentException("Bad FormMode");
            }
        }

        // вернет true если текст пустой
        private static bool ValidateOnEmptyAndWhiteSpaces(string text, string label)
        {
            if (string.IsNullOrEmpty(text) || string.IsNullOrWhiteSpace(text))
            {
                MessageBox.Show($"{label} не должно быть пустым!");
                return true;
            }
            return false;
        }

        // проверяет есть ли на сервере комнаты с таким же названием
        // если есть то вернет true
        private static async Task<bool> CheckRoomName(string roomName)
        {
            await ConnectionContext.InvokeCheckRoomName(roomName);
            if (ConnectionContext.ResultExistRoomByName)
            {
                MessageBox.Show($"Комната с названием [{roomName}] уже сущетвует");
                return true;
            }
            return false;
        }

        // проверяет нет ли в комнате пользователя с таким именем
        // если есть то вернет true
        private static async Task<bool> CheckUsernameOnRoom(string username, string secretKey)
        {
            await ConnectionContext.InvokeCheckUsername(username, secretKey);
            if (ConnectionContext.UsernameExistInRoom == true)
            {
                MessageBox.Show("Данное имя занято, придумайте другое");
                return true;
            }
            return false;
        }
    }
}