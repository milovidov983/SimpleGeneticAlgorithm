using AiApplication.Models;
using AIv2;
using Otter.Core;
using Otter.Graphics.Drawables;
using System.IO;

namespace AiApplication {
	public class BotUpdater : Entity {
		private readonly Scene scene;
		private readonly string savedCode;
		private Bot[] winners = null;
		private SimpleStateMachine context;
		private UnitController unitController;
	

		private bool IsIterationComplete { get { return context?.HasWinner == true; } }
		private bool IsNeedToStartInitialization { get { return context?.IsRunning is null || context?.IsRunning == false; } }

		public BotUpdater(Scene scene) {
			this.scene = scene;
			savedCode = File.ReadAllText("code-gen-17.json");
		}

		
		public override void Update() {
			base.Update();

			if (IsIterationComplete) {
				winners = context.GetWinners();
				IncrementGeneration(winners);
				ResetStates(winners);

				unitController.ClearEntities();
			}

			if (IsNeedToStartInitialization) {
				var map = new MapImplementation();

				unitController = new UnitController(scene, map);
				map.CreateAndFillWorldObjects();

				var commandFactory = new CommandFactory(map);
				var botFactory = new BotFactory(commandFactory);
				var bots = botFactory.CreateBots(winners, savedCode);

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

