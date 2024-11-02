using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace MessagingBackend {
    public class WebSocketManager
    {
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public void AddSocket(string name, WebSocket socket)
        {
            _sockets.TryAdd(name, socket);
        }

        public void RemoveSocket(string id)
        {
            _sockets.TryRemove(id, out _);
        }

        public ICollection<string> GetAllSocketIds() {
            return _sockets.Keys;
        }

        public async Task SendMessageToAll(string message)
        {
            var buffer = System.Text.Encoding.UTF8.GetBytes(message);
            var segment = new ArraySegment<byte>(buffer);
            foreach (var socket in _sockets.Values)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(segment, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
