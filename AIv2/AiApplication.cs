using System;
using System.Linq;

namespace AIv2 {


	public class AiApplication {
		private const int LOOP_COUNT = 10000;


		public void Start() {
			Bot[] winners = null;
			int counter = 0;
			while (LOOP_COUNT > counter++) {
				Console.WriteLine("Создание карты..");
				var map = new MapImplementation();
				map.CreateAndFillWorldObjects();
				map.Add(EventController.Instance);
				Console.WriteLine("Карта инициализирована.");

				Console.WriteLine("Создание фабрики команд..");
				var commandFactory = new CommandFactory(map);
				Console.WriteLine("Создание фабрики ботов..");
				var botFactory = new BotFactory(commandFactory);
				Console.WriteLine("Создание ботов..");
				Bot[] bots = botFactory.CreateBots(winners);
				Console.WriteLine("Боты созданы успешно");

				Console.WriteLine("Устанавливаем ботов на карту");
				map.Add(bots);
				Console.WriteLine("Боты на карту нанесены");

				Console.WriteLine("Создаем контекст выполнения");
				var executionContext = new Context(map, bots);
				Console.WriteLine("Запускаем контекст выполнения");
				executionContext.Run();
				
				winners = executionContext.GetWinners();
				if (counter % 5 == 0) {
					PrintGenerations(winners);
					Console.ReadLine();
				}

				IncrementGeneration(winners);
				ResetStates(winners);
			}
		}

		private void ResetStates(Bot[] winners) {
			foreach(var bot in winners) {
				bot.ResetState();
			}
		}

		private void IncrementGeneration(Bot[] winners) {
			foreach(var bot in winners) {
				bot.Generation++;
			}
		}
		private void PrintGenerations(Bot[] bots) {
			var gens = bots
				.OrderBy(x => x.Generation)
				.Select(x=> $"[GEN:{x.Generation} GENOME:{x.GenomeId}:  {x.Id}]\n")
				.ToArray();

			var genomes = bots
					.OrderBy(x => x.GenomeId)
					.Select(x => x.GenomeId)
					.ToArray();

			Console.WriteLine("---------------------------------------------------------------");
			Console.WriteLine($"Выжившие поколения:\n {string.Join(" ", gens)}\n");
			Console.WriteLine($"Выжившие геномы: {string.Join(" ", genomes)}");
		}
	}
}