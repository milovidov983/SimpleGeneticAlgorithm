using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLiveAi.Handlers;
using WebApiLiveAi.Services;
using WebApiLiveAi.Sockets;

namespace WebApiLiveAi {
	public class Startup {

		public void ConfigureServices(IServiceCollection services) {
			services.AddWebSocketManager();
			services.AddSingleton<ApplicationService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
			if (env.IsDevelopment()) {
				app.UseDeveloperExceptionPage();
			}

			var serviceScopeFactory = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
			var serviceProvider = serviceScopeFactory.CreateScope().ServiceProvider;

			app.UseWebSockets();
			app.MapWebSocketManager("/ws", serviceProvider.GetService<ChatMessageHandler>());
			app.UseStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints => {
				endpoints.MapGet("/", async context => {
					await context.Response.WriteAsync("Hello World!");
				});
			});
		}
	}
}
