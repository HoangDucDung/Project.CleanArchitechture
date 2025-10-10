namespace Project.Middelware.Middlewares
{
    public class AuthenMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            // Xử lý trước khi chuyển request đến middleware tiếp theo
            Console.WriteLine("Trước khi xử lý request");
            // Chuyển request đến middleware tiếp theo
            await _next(context);
            // Xử lý sau khi nhận response từ middleware tiếp theo
            Console.WriteLine("Sau khi xử lý request");
        }
    }
}
