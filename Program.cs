using WareHouse.Server.Services;

namespace WareHouse.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<ErrorHandlingInterceptor>();

            // Add services to the container.
            builder.Services.AddGrpc(op => {

                op.Interceptors.Add<ErrorHandlingInterceptor>();
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.MapGrpcService<WareHouseService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}