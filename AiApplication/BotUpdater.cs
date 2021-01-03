
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
		private const int LOOP_COUNT = 2000;
		private readonly Scene scene;
		private int counter;
		private Bot[] winners = null;

		public BotUpdater(Scene scene) {
			this.scene = scene;
			this.counter = 0;
		}

		private SimpleStateMachine context;
		private UnitController unitController;
		public override void Update() {
			base.Update();

			if (context?.HasWinner == true) {
				winners = context.GetWinners();
				IncrementGeneration(winners);
				ResetStates(winners);

				unitController.ClearEntities();
			}
			if (context?.IsRunning is null || context?.IsRunning == false && LOOP_COUNT > counter++) {
				var map = new MapImplementation();
				unitController = new UnitController(scene, map);
				map.CreateAndFillWorldObjects();

				var commandFactory = new CommandFactory(map);
				var botFactory = new BotFactory(commandFactory);
				var bots = botFactory.CreateBots(winners);

				map.Add(bots);
				context = new SimpleStateMachine(bots, map);

				context.Start();
			} else {
				context?.ExecuteBotStep();
			}

		}

		private void ResetStates(Bot[] winners) {
			foreach (var bot in winners) {
				bot.ResetState();
			}
		}

		private void IncrementGeneration(Bot[] winners) {
			foreach (var bot in winners) {
				bot.Generation++;
			}
		}

	}
}

