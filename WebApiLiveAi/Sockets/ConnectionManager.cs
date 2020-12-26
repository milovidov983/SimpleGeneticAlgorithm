using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebApiLiveAi.Sockets {
    public class ConnectionManager {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public WebSocket GetSocketById(string id) {
            _sockets.TryGetValue(id, out var socket);
            return socket;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll() {
            return _sockets;
        }

        public string GetId(WebSocket socket) {
            return _sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        public void AddSocket(WebSocket socket) {
            _sockets.TryAdd(CreateConnectionId(), socket);
        }

        public async Task RemoveSocket(string id) {
            _sockets.TryRemove(id, out var socket);

            if (socket?.State != WebSocketState.Open) {
                return;
            }

            await socket?.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the ConnectionManager",
                                    cancellationToken: CancellationToken.None);
        }

        private static string CreateConnectionId() {
            return Guid.NewGuid().ToString();
        }
    }
}
