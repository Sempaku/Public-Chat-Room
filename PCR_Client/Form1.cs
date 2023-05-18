namespace PCR_Client
{
    public partial class Form1 : Form
    {
        public static Form Form = null!;
        public static ListBox listBox = null!;

        public Form1()
        {
            InitializeComponent();
            listBox = messages_listBox;
            Form = this;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (Program.CurrentRoomname == "")
                await Task.Delay(10000);
            Text = $"Public Chat Room |         [Room name] : {Program.CurrentRoomname}   [Username] : {Program.CurrentUsername}";
            await ConnectionContext.InvokeGetHistory(Program.CurrentSecretKey);
        }

        // отправка сообщения на сервер
        private async void send_button_Click(object sender, EventArgs e)
        {
            try
            {
                string message = message_textBox.Text;
                if (string.IsNullOrEmpty(message) || string.IsNullOrWhiteSpace(message))
                {
                    MessageBox.Show("Поле сообщения не должно быть пустым!");
                    return;
                }
                await ConnectionContext.InvokeSendMessage(Program.CurrentUsername, message, Program.CurrentSecretKey);
                message_textBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error sending message: {ex.Message}");
            }
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await ConnectionContext.InvokeNotify("LeaveFromRoom",
                new object[] { Program.CurrentUsername, Program.CurrentSecretKey });
            await ConnectionContext.StopAsync();
        }
    }
}