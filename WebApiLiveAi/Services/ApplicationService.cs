using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLiveAi.Handlers;

namespace WebApiLiveAi.Services {
	public class ApplicationService {
        private NotificationsMessageHandler notificationsMessageHandler;

        public ApplicationService(NotificationsMessageHandler notificationsMessageHandler) {
            this.notificationsMessageHandler = notificationsMessageHandler;
        }


        public async Task SendMessage(string message) {
            await notificationsMessageHandler.SendMessageToAllAsync(message);
        }
    }
}
