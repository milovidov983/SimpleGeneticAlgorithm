﻿using AiLib.Shared;
using System;
using System.Text;

namespace AIv2 {
	

	public class Bot : IItem {
		private int actionCounter;
		private int healthScore;
		private Position position;
		private Position cursor;
		private int generation = 1;

		private readonly CommandFactory commandFactory;


		public event ChangePosition PositionChangedEvent;
		public event CommonEvent EndOfHealthEvent;
		public event SetGeneration SetGeneration;

		public readonly Guid Id = Guid.NewGuid();
		public Brain Brain { get; private set; }
		public int Generation {
			get => generation;
			set {
				generation = value;
				SetGeneration?.Invoke(value);
			} 
		}
		public int GenomeCount { get; set; } = 1;
		public int HealthScore {
			get => healthScore;
			set {
				healthScore = value;
				if (healthScore > Settings.BOT_HEALTH_LIMIT) {
					healthScore = Settings.BOT_HEALTH_LIMIT;
				}
				if (healthScore <= 0) {
					EndOfHealthEvent.Invoke(Id, Position);
				}
			}
		}
		public bool IsAlive { get => HealthScore > 0; }
		public Position Position {
			get => position;
			set {
				if (value != null && position != null) {
					PositionChangedEvent.Invoke(
						id: Id,
						newPosition: value,
						oldPosition: position);
				}

				position = value;
			}
		}
		public Position Cursor {
			get => cursor;
			set => cursor = value;
		}

		public int Direction { get; set; }

		public int StepCounter { get; set; } = 0;

		public Bot(Brain brain, CommandFactory commandFactory) {
			this.Brain = brain;
			this.commandFactory = commandFactory;
			HealthScore = Settings.INIT_HEALTH_COUNT;
		}

		public void SetNewPosition(Position newPosition) {
			if (IsAlive) {
				Position = newPosition;
			}
		}


		private BotHandleStratagy currentHandleStratagy;
		private bool logEnabled;// { get => Generation > 7; }
		

		public void Handle(WorldObject worldObject) {
			if (IsAlive) {
				currentHandleStratagy.Handle(worldObject, this);
			}
		}


		public void Execute() {
			if (!IsAlive) {
				return;
			}
			actionCounter = 0;
			bool running = true;
			while (running && HasActionScore()) {
				actionCounter++;

				var currentCommand = Brain.GetNextCommand();
				if (IsFinalCommand(currentCommand)) {
					running = false;
				}

				ICommand commandImplementation = commandFactory.GetCommand(currentCommand);
				currentHandleStratagy = commandImplementation.CreateHandleStratagy();
				commandImplementation.Execute(this);

				if (!IsAlive) {
					running = false;
				}
				HealthScore--;

				if (logEnabled) {
					var item = LogItemFactory.CreateFrom(this, currentCommand, commandImplementation.GetType().Name);
					Logger.Instance.logs.Enqueue(item);
				}
				StepCounter++;
			}
		}

		public bool HasActionScore() {
			return actionCounter < Settings.MAX_ACTION_COUNT_PER_STEP;
		}

		public void InitPosition(int x, int y) {
			Position = new Position(x, y);
		}

		public void ResetState() {
			Position = null;
			HealthScore = Settings.INIT_HEALTH_COUNT;
			actionCounter = 0;
			Unsubscribe();
		}

		private bool IsFinalCommand(int commandId) {
			return MakeStep.IsFitToCommand(commandId);
		}


		private Action Unsubscribe;
		public void AddSububscriptions(IBotEventObserver botEventObserver) {
			EndOfHealthEvent += botEventObserver.SetDead;
			PositionChangedEvent += botEventObserver.SetMove;
			SetGeneration += botEventObserver.UpGeneration;

			this.Unsubscribe = () => {
				PositionChangedEvent -= botEventObserver.SetMove;
				EndOfHealthEvent -= botEventObserver.SetDead;
				SetGeneration -= botEventObserver.UpGeneration;
			};

			EndOfHealthEvent += (Guid id, Position position) => {
				Unsubscribe();
			};
		}


		

	}
}