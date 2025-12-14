using Project.Controller.Host.Factorys;
using Project.Host.Base.Configs;
using System.Reflection;
using Project.Host.Base.Bases;

namespace Project.Controller.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddBaseConfiguration(
            [
                "auth.json",
                "connection.json"
            ]);


            var docName = "Auth";

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Thêm dịch vụ tạo tài liệu API
            builder.Services.AddAPIDocument(Assembly.GetExecutingAssembly().GetName().Name ?? "", docName);

            // Đăng ký các dịch vụ tùy chỉnh
            builder.Services.AddLazyloadFactory();
            builder.Services.UseAppAuthenExtensionFactory();
            builder.Services.UserDomainManagerServiceFactory();

            // Đăng ký các options
            builder.Services.Configure<AuthConfig>(builder.Configuration);
            builder.Services.Configure<ConnectionConfig>(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
                app.UseAPIDocument(docName);

            // Sử dụng middleware tùy chỉnh
            app.MiddlewareRegistration();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}