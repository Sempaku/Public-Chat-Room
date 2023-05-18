namespace PCR_Client.Forms
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
            //Text = "Стартовое окно";
        }

        private void CreateRoom_button_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new RoomForm(mode: "CreateRoom");
            Program.Context.MainForm.Show();
            Close();
        }

        private void JoinToRoom_button_Click(object sender, EventArgs e)
        {
            Program.Context.MainForm = new RoomForm(mode: "JoinToRoom");
            Program.Context.MainForm.Show();
            Close();
        }
    }
}