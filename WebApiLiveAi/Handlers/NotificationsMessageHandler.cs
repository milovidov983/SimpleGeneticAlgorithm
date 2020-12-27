using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebApiLiveAi.Sockets;

namespace WebApiLiveAi.Handlers {
    public class NotificationsMessageHandler : WebSocketHandler {
        public NotificationsMessageHandler(
            ConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager) {
        }

        public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer) {
            throw new NotImplementedException();
        }

        public override Task OnConnected(WebSocket socket) {
            base.OnConnected(socket);

            

            return Task.CompletedTask;
		}
    }
}
