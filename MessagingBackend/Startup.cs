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

            app.UseWebSockets();

            app.UseRouting();
            app.UseCors("AllowAllOrigins");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseWebSockets();

            app.Use(async (context, next) =>
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    var socketId = NameGenerationService.GetUniqueSocketId();
                    var currentNames = webSocketManager.GetAllSocketIds();
                    while (currentNames.Contains(socketId)) {
                        socketId = NameGenerationService.GetUniqueSocketId();
                    }

                    webSocketManager.AddSocket(socketId, webSocket);

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

            var myBytes = System.Text.Encoding.UTF8.GetBytes(socketId);
            var segment = new ArraySegment<byte>(myBytes);

            await webSocket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);

            try
            {
                while (webSocket.State == WebSocketState.Open)
                {
                    result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    // receive msgs
                }
            }
            catch (Exception)
            {
                // exceptions lol
            }
            finally
            {
                webSocketManager.RemoveSocket(socketId);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
            }
        }

    }
}
