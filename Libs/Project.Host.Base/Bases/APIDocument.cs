
using Microsoft.OpenApi.Models;

namespace Project.Host.Base.Bases
{
    public static class APIDocument
    {
        public static IServiceCollection AddAPIDocument(this IServiceCollection services, string filePath, string docName)
        {
            services.AddSwaggerGen(options =>
            {
                var xmlFilename = $"{filePath}.xml";
                options.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = $"{docName} Documentation",
                        Version = "v1",
                        Description = $"Tài liệu tích hợp {docName.ToLower()}",
                        Contact = new OpenApiContact()
                        {
                            Name = "Hoàng Đức Dũng",
                            Email = "abc@gmail.com",
                            Url = new Uri("https://abc.xyz/")

                        },
                        //Extensions = new Dictionary<string, IOpenApiExtension>
                        //{
                        //    {"x-logo", new OpenApiObject
                        //        {
                        //            {"url", new OpenApiString("https://scontent.fhan14-3.fna.fbcdn.net/v/t39.30808-1/514698492_1415732179453094_3522297410227070367_n.jpg?stp=dst-jpg_s200x200_tt6&_nc_cat=103&ccb=1-7&_nc_sid=1d2534&_nc_ohc=YVGh5EDLKgkQ7kNvwGnBxbZ&_nc_oc=Adlddbkpck2aMRaEh0PQPP7qEiDQvncaY0Gh5q_ud5XL7p4nnGembjZRuDiwyq8WvQg&_nc_zt=24&_nc_ht=scontent.fhan14-3.fna&_nc_gid=MrwWdVlg4DTgn5JoAJtwSw&oh=00_Afdv6E2pyYxhaX3s4LoFD7zWDFM3gI6ljxmzuxPnImNl2w&oe=68E9D54D")},
                        //        }
                        //    }
                        //}
                    });

                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

            });
            return services;
        }

        public static IApplicationBuilder UseAPIDocument(this IApplicationBuilder app, string docName)
        {
            app.UseSwagger();
            app.UseSwaggerUI();

            // Cấu hình ReDoc làm mặc định
            app.UseReDoc(c =>
            {
                c.RoutePrefix = "docs"; // Đặt làm route mặc định
                c.SpecUrl = "/swagger/v1/swagger.json";
                c.DocumentTitle = $"{docName} Documentation";
                c.HideDownloadButton();
            });

            return app;
        }
    }
}
