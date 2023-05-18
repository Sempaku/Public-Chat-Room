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
            // Этот код добавляет маршрутизацию для нашего ChatHub и указывает,
            // что клиенты должны подключаться к URL /chathub.
            app.MapHub<ChatHub>("/chatH");

            app.Run();
        }
    }
}