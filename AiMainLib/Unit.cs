using System;
using System.Collections.Generic;
using System.Text;

namespace AiMainLib {
	interface IUnit {
		Position GetCurrentPosition();
		void SetNewPosition(Position newPosition);
		int CurrentCommandIndex { get; set; }
		void ExecuteAction(ICommand command);
	}
	public delegate void ChangePosition(Position oldPosition, Position newPosition);
	public delegate void StopExecution();
	class Unit : IUnit {
		public const int CODE_SIZE = 64;
		private const int MAX_ACTION_COUNT_PER_STEP = 10;
		private const int INIT_HEALTCH_COUNT = 64;

		public event ChangePosition PositionChanged;
		public event StopExecution StepFinished;


		public int[] Code { get; set; } = new int[CODE_SIZE];
		public int CurrentCommandIndex { get; set; }

		public int HealtScore { get; set; } = INIT_HEALTCH_COUNT;
		private bool isAlive { get => HealtScore > 0; }

		public int ActionCount { get; set; } = 0;

		private Position currentPosition;
		private readonly IWorld world;

		

		


		public void ExecuteAction(ICommand command) {
			if (ActionCount < MAX_ACTION_COUNT_PER_STEP) {
				ActionCount++;
				command.Execute(this, CurrentCommandIndex);


			} else {
				ActionCount = 0;
				StepFinished.Invoke();
			}
			if (isAlive) {
				ChangeCommand();
			}
		}
		protected virtual void ChangeCommand() {
			if (CurrentCommandIndex < CODE_SIZE) {
				CurrentCommandIndex++;
			} else {
				CurrentCommandIndex = 0;
			}
		}


		public void ChangePosition(Position newPos) {
			world.MoveElement(from: currentPosition, to: newPos);

			currentPosition = newPos;
		}

		public Position GetCurrentPosition() {
			return currentPosition;
		}

		public void SetNewPosition(Position newPosition) {
			var worldElement = world.GetObjectFrom(newPosition);
			switch (worldElement) {
				case Wall w:
					ExecuteAdditionalAction(w, newPosition);
					return;
				case Food f:
					ExecuteAdditionalAction(f, newPosition);
					return;
				case Bot o:
					ExecuteAdditionalAction(o, newPosition);
					return;
				case Poison p:
					ExecuteAdditionalAction(p, newPosition);
					return;
				case Empty e:
					ExecuteAdditionalAction(e, newPosition);
					break;
				default:
					throw new Exception("Unknown type of world object");
			}

			currentPosition = newPosition;
		}

		private void ExecuteAdditionalAction(WorldObject worldObject, Position position) {
			
		}
	}
}
