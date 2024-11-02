using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using System.Threading.Tasks;
using System.Threading;
using System.Net.WebSockets;
using System;

namespace MessagingBackend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<WebSocketManager>();
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, WebSocketManager webSocketManager)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseWebSockets(); // Enable WebSockets

            app.UseRouting();
            app.UseCors("AllowAllOrigins"); // Use the CORS policy

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers(); // Maps controller routes
            });

            app.UseWebSockets(); // Enable WebSockets

            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    string socketId = Guid.NewGuid().ToString(); // Generate a unique ID for the socket
                    webSocketManager.AddSocket(socketId, webSocket);

                    // Handle incoming messages and connection close
                    await HandleWebSocket(socketId, webSocket, webSocketManager);
                }
                else
                {
                    await next();
                }
            });
        }

        private async Task HandleWebSocket(string socketId, WebSocket webSocket, WebSocketManager webSocketManager)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult? result = null;

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    // You can handle messages here if needed
                }
            }
            catch (Exception)
            {
                // Handle exceptions as necessary
            }
            finally
            {
                webSocketManager.RemoveSocket(socketId);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }

    }
}
