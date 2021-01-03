using AiLib;
using AIv2;
using Appccelerate.StateMachine;
using Appccelerate.StateMachine.AsyncMachine;
using System;
using System.Linq;

namespace AiApplication {
	public class SimpleStateMachine {
		private enum States {
			BotExecution,
			AddObject,
			CalculateWinner,
			IsFinish
		}
		private enum Events {
			BotExecuted,
			NoWinners,
			HasWinners,
			BotsDone,
			FoodAdded,
			PoisonAdded
		}

		private readonly AsyncPassiveStateMachine<States, Events> machine;

		private readonly Bot[] bots;
		private readonly MapImplementation map;
		private int objectAddedCounter = 0;
		private int currentBotIndex = 0;
		private int totalSteps;
		private static int totalStepsMax = 0;
		private static int totalIteration = 0;

		public bool HasWinner { get; private set; }
		public bool IsRunning { get; private set; }

		public SimpleStateMachine(Bot[] bots, MapImplementation map) {
			this.bots = bots;
			this.map = map;

			var builder = new StateMachineDefinitionBuilder<States, Events>();

			builder
				.In(States.BotExecution)
				.On(Events.BotExecuted)
				.Goto(States.CalculateWinner)
				.Execute(CalculateWinners);

			builder
				.In(States.CalculateWinner)
				.On(Events.NoWinners)
				.Goto(States.BotExecution);			
			
			builder
				.In(States.CalculateWinner)
				.On(Events.HasWinners)
				.Goto(States.IsFinish)
				.Execute(Stop);

			builder
				.In(States.BotExecution)
				.On(Events.BotsDone)
					.If(CanAddObjects)
						.Goto(States.AddObject)
						.Execute(AddFood);

			builder
				.In(States.AddObject)
				.On(Events.FoodAdded)
				.Goto(States.AddObject)
				.Execute(AddPoison);

			builder
				.In(States.AddObject)
				.On(Events.PoisonAdded)
				.Goto(States.BotExecution);

			builder.WithInitialState(States.BotExecution);

			machine = builder.Build().CreatePassiveStateMachine();
		}
		public bool HasWinners() {
			var botCount = bots.Count(x => x.IsAlive);
			HasWinner = botCount <= AIv2.Settings.WINNER_MAX_COUNT;
			return HasWinner;
		}
		public void Start() {
			IsRunning = true;
			machine.Start();

		}

		public void ExecuteBotStep() {
			if (bots.Length > currentBotIndex) {
				bots[currentBotIndex].Execute();
				machine
					.Fire(Events.BotExecuted);
				currentBotIndex++;
			} else {
				currentBotIndex = 0;
				machine
					.Fire(Events.BotsDone);
				totalSteps++;
				totalStepsMax = Math.Max(totalStepsMax, totalSteps);
			}
		}

		private void Stop() {
			IsRunning = false;
			machine.Stop();
			PrintGenerations(GetWinners());
			var logger = Logger.Instance;
			totalIteration++;
			logger.Print();
		}

		private void PrintGenerations(Bot[] bots) {
			var gens = bots
				.OrderBy(x => x.Generation)
				.Select(x => $"[ GENERATION: {x.Generation} | GENOME: {x.GenomeCount} | ID: {x.Id} ]\n")
				.ToArray();

			var genomes = bots
					.OrderBy(x => x.GenomeCount)
					.Select(x => x.GenomeCount)
					.ToArray();

			Console.WriteLine("---------------------------------------------------------------");
			Console.WriteLine($"Выжившие поколения:\n {string.Join(" ", gens)}\n");
			Console.WriteLine($"Общее количество ходов в текущей итерации: {totalSteps}");
			Console.WriteLine($"Максимальное количество ходов за итерацию: {totalStepsMax}");
			Console.WriteLine($"Общее количество итераций: {totalIteration}");
		}
		private bool CanAddObjects() {
			return objectAddedCounter++ % Settings.ADD_OBJ_PER_ITERATIONS == 0;
			
		}
		private void CalculateWinners() {
			if (HasWinners()) {
				machine
					.Fire(Events.HasWinners);
			} else {
				machine
					.Fire(Events.NoWinners);
			}

		}
		private void AddFood() {
			map.AddObjects(new WorldObjectFactory(map.food), Settings.ADD_OBJECT_COUNT);
			machine
				.Fire(Events.FoodAdded);
		}
		private void AddPoison() {
			map.AddObjects(new WorldObjectFactory(map.poison), Settings.ADD_OBJECT_COUNT);
			machine
				.Fire(Events.PoisonAdded);
		}



		public Bot[] GetWinners() {
			return bots.Where(x => x.IsAlive).ToArray();
		}
	}
}
