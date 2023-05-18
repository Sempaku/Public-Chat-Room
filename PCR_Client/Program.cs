using PCR_Client.Forms;
using Message = PCR_Client.Models.Message;

namespace PCR_Client
{
    internal static class Program
    {
        #region props

        public static ApplicationContext Context { get; set; } = null!;
        public static ConnectionContext ConnectionContext { get; set; } = null!;
        public static string CurrentSecretKey { get; set; } = string.Empty;
        public static string CurrentUsername { get; set; } = string.Empty;
        public static string CurrentRoomname { get; set; } = string.Empty;

        public static List<Message> MessagesHistory { get; set; } = null!;

        #endregion props

        [STAThread]
        private static async Task Main()
        {
            ApplicationConfiguration.Initialize();

            // Init props
            ConnectionContext = new ConnectionContext();
            MessagesHistory = new List<Message>();

            // Start connection & Receive methods
            await ConnectionContext.StartAsync();
            ConnectionContext.ReceiveAll();

            // Init Form Context & Run app
            Context = new ApplicationContext(new StartForm());
            Application.Run(Context);
        }
    }
}