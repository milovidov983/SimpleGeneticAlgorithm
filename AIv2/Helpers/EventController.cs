using System;

namespace AIv2 {
	public class EventController : IWorldObjectEvents {
		public event ChangePosition PositionChangedEvent;
		public event StopExecution StepFinished;
		public event CommonEvent EndOfHealthEvent;

		public static EventController Instance { get; private set; }

		public void EndOfHealth(Position position) {
			EndOfHealthEvent.Invoke(position);
		}

		public void PositionChanged(Position newPosition, Position oldPosition) {
			PositionChangedEvent.Invoke(newPosition, oldPosition);
		}

		static EventController() {
			Instance = new EventController();
		}
	}
}