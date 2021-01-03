﻿using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace AIv2 {


	public class AiApplication {
		private const int LOOP_COUNT = 25000;
		private int maxGeneration = 0;

		public List<Statistics> statistics = new List<Statistics>();

		public void Start() {
			Bot[] winners = null;
			int counter = 0;
			System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
			sw.Start();
			while (LOOP_COUNT > counter++) {
				var map = new MapImplementation();
				map.CreateAndFillWorldObjects();
				

				var commandFactory = new CommandFactory(map);
			
				var botFactory = new BotFactory(commandFactory);

				Bot[] bots = botFactory.CreateBots(winners);

				map.Add(bots);
			
				var executionContext = new Context(map, bots);
				//var exeCOntextSw = System.Diagnostics.Stopwatch.StartNew();
				executionContext.Run();
				//exeCOntextSw.Stop();
				//Console.WriteLine($"executionContext.Run() - {exeCOntextSw.ElapsedMilliseconds}");
				
				winners = executionContext.GetWinners();
				//Console.SetCursorPosition(0, 0);
				//Console.Write(counter);
				if (counter % 1000 == 0) {
					sw.Stop();

					Console.WriteLine($"{LOOP_COUNT}/{LOOP_COUNT - counter} iterations...");
					PrintGenerations(winners);
					Console.WriteLine($"Elapsed time: {sw.Elapsed.ToString(@"hh\:mm\:ss")}");
					Console.WriteLine($"Max generation is {maxGeneration}");
					WriteStatisticToStorage(counter.ToString());

					sw.Restart();
				}

				SaveCode(winners);


				statistics.AddRange(bots.Select(x => new Statistics {
					Generation = x.Generation,
					Iteration = counter,
					StepCount = x.StepCounter
				}));

				IncrementGeneration(winners);
				ResetStates(winners);
			}
			WriteStatisticToStorage("final");
		}

		private void SaveCode(Bot[] winners) {
			if(winners?.Any() != true) {
				return;
			}

			var botMaxGen = winners.OrderByDescending(x => x.Generation).First();
			if(botMaxGen.Generation > maxGeneration) {
				maxGeneration = botMaxGen.Generation;
				var code = JsonSerializer.Serialize(botMaxGen.Brain.Code);
				File.WriteAllText($"code-gen-{botMaxGen.Generation}.json", code);
			}
		}

		public void WriteStatisticToStorage(string postfix) {
			using (var writer = new StreamWriter($"output_{postfix}.csv"))
			using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture)) {
				csv.WriteRecords(statistics);
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
				.Select(x=> $"[ GENERATION: {x.Generation} | GENOME: {x.GenomeCount} | ID: {x.Id} ]\n")
				.ToArray();

			var genomes = bots
					.OrderBy(x => x.GenomeCount)
					.Select(x => x.GenomeCount)
					.ToArray();

			Console.WriteLine("---------------------------------------------------------------");
			Console.WriteLine($"Выжившие поколения:\n {string.Join(" ", gens)}\n");
		}
	}

	public class Statistics {
		public int StepCount { get; set; }
		public int Generation { get; set; }
		public int Iteration { get; set; }
	}
}