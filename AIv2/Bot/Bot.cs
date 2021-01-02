using System;

namespace AIv2 {
	public class Bot {
		private int actionCounter;
		private int healthScore;
		private Position position;
		private Position cursor;
		
		private readonly CommandFactory commandFactory;

		public event ChangePosition PositionChangedEvent;
		public event StopExecution StepFinished;
		public event CommonEvent EndOfHealthEvent;


		public void AddSub(IBotEventObserver botEventObserver) {
			EndOfHealthEvent += botEventObserver.SetDead;
			PositionChangedEvent += botEventObserver.SetMove;

			EndOfHealthEvent += (Guid id, Position position) => {
				PositionChangedEvent -= botEventObserver.SetMove;
				EndOfHealthEvent -= botEventObserver.SetDead;
			};
		}

		public readonly Guid Id = Guid.NewGuid();
		public Brain Brain { get; private set; }
		public int Generation { get; set; }
		public int GenomeCount { get; set; } = 1;
		public int HealthScore {
			get => healthScore;
			set {
				if (healthScore < Settings.BOT_HEALTH_LIMIT) {
					healthScore = value;
				}
				if (healthScore <= 0) {
					EndOfHealthEvent.Invoke(Id,Position);
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


		private WorldObjectHandleStratagy currentHandleStratagy;
		private bool logEnabled;

		public void Handle(WorldObject worldObject) {
			currentHandleStratagy.Handle(worldObject, this);
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
		}

		private bool IsFinalCommand(int commandId) {
			return MakeStep.IsFitToCommand(commandId);
		}


		public void EnableLog() {
			this.logEnabled = true;
		}

	}
}