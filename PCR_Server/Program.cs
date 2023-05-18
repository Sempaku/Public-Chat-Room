namespace PCR_Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSignalR();

            var app = builder.Build();

            app.UseRouting();
            // ���� ��� ��������� ������������� ��� ������ ChatHub � ���������,
            // ��� ������� ������ ������������ � URL /chathub.
            app.MapHub<ChatHub>("/chatH");

            app.Run();
        }
    }
}