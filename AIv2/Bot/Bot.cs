using System;

namespace AIv2 {
	public class Bot {
		private int actionCounter;
		private int healthScore;
		private Position position;
		private readonly EventController eventController = EventController.Instance;
		private readonly CommandFactory commandFactory;

		public Guid Id = Guid.NewGuid();
		public Brain Brain { get; private set; }
		public int Generation { get; set; }
		public int GenomeId { get; set; } = 1;
		public int HealthScore {
			get => healthScore;
			set {
				if (healthScore < Settings.BOT_HEALTH_LIMIT) {
					var id = Id;
					healthScore = value;
				}
				if (healthScore <= 0) {
					eventController.EndOfHealth(Position);
				}
			}
		}
		public bool IsAlive { get => HealthScore > 0; }
		public Position Position { 
			get => position;
			set {
				if (value != null && position != null) {
					eventController.PositionChanged(
						newPosition: value,
						oldPosition: position);
				}

				position = value;
			}
		}

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
		public void Handle(WorldObject worldObject) {
			currentHandleStratagy.Handle(worldObject, this);
		}

		public void Execute() {
			var id = Id;
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

	}
}