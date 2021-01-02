
using AiApplication.Models;
using AiLib.Presentor;
using AIv2;
using Otter.Core;
using Otter.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AiApplication {


	public class BotUpdater : Entity {
		private List<PlayerEntity> all = new List<PlayerEntity>();
		private Context context;
		private readonly Scene scene;

		public BotUpdater(Scene scene) {
			this.scene = scene;
		}

		public override void Update() {
			base.Update();
			context.Run();
		}


		public void Init(Bot[] bots, Context context) {
			this.context = context;
			var factory = BotViewFactory.Instance;
			foreach (var bot in bots) {
				var entity = factory.Create(bot);
				all.Add(entity);
				scene.Add(entity);
			}
		}
	}
}

