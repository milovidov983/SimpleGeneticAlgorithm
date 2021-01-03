using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {
	public class BotFactory {
		private readonly CommandFactory commandFactory;

		public BotFactory(CommandFactory commandFactory) {
			this.commandFactory = commandFactory;
		}

		public Bot[] CreateBots(Bot[] bots = null) =>
			 bots?.Any() != true
				? InitRandom()
				: InitFromExistingGenome(bots);
		

		private Bot[] InitFromExistingGenome(Bot[] parentsBots) {
			var botsList = new List<Bot>();
			var childCount = Settings.BOT_COUNT / parentsBots.Length;
			foreach (var parentBot in parentsBots) {
				var childs = Enumerable.Range(0, childCount - 1).Select(x => {
					var brain = new Brain();
					brain.Init(parentBot.Brain.Code);
					var newBot = new Bot(brain, commandFactory);
					newBot.GenomeCount += parentBot.GenomeCount + 1;
					return newBot;
				}).Union(new[] { parentBot }).ToArray();

				MakeMutation(childs.First().Brain.Code);
				botsList.AddRange(childs);
			}

			return botsList.ToArray();
		}

		private void MakeMutation(int[] gencode) {
			var random = new Random((int)DateTime.Now.Ticks);
			var swapIndexA = random.Next(0, gencode.Length / 2);
			var swapIndexB = random.Next(gencode.Length / 2, gencode.Length - 1);

			var tempValue = gencode[swapIndexA];
			gencode[swapIndexA] = gencode[swapIndexB];
			gencode[swapIndexB] = tempValue;
		}

		private Bot[] InitRandom() {
			return Enumerable.Range(0, Settings.BOT_COUNT).Select(x => {
				var generator = new Generator(Settings.CODE_SIZE);
				var genome = Enumerable.Range(0, Settings.CODE_SIZE).Select(x => generator.Get()).ToArray();
				var brain = new Brain();
				brain.Init(genome);
				return new Bot(brain, commandFactory);
			}).ToArray();
		}

	}
}