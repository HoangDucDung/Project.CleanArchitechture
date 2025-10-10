
using Project.Host.Base.Factories;
using Project.Middelware;
using System.Reflection;

namespace Project.Controller.Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Thêm dịch vụ tạo tài liệu API
            builder.Services.AddAPIDocument(Assembly.GetExecutingAssembly().GetName().Name ?? "");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseAPIDocument();
            }

            // Sử dụng middleware tùy chỉnh
            app.UseCustomMiddleware();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
