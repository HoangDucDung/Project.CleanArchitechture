
using Project.Controller.Host.Factorys;
using Project.Host.Base.Bases;
using Project.Infastructure.Kafka;
using System.Reflection;
using Project.Host.Base.Configs;

namespace Project.Controller.Test
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddBaseConfiguration(
            [
                "kafka.json"
            ]);


            var docName = "Test";

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            // Thêm dịch vụ tạo tài liệu API
            builder.Services.AddAPIDocument(Assembly.GetExecutingAssembly().GetName().Name ?? "", docName);

            // Đăng ký các dịch vụ tùy chỉnh
            builder.Services.UseMessageBrokerFactory();
            builder.Services.AddLazyloadFactory();

            // Đăng ký các options
            builder.Services.GetKafkaConfig(builder.Configuration);
            builder.Services.GetProducerCommonConfig(builder.Configuration);

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
