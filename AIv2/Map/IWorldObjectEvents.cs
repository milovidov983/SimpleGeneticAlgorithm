using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public delegate void ChangePosition(Guid id, Position oldPosition, Position newPosition);
	public delegate void StopExecution();
	public delegate void CommonEvent(Guid id, Position position);
	public interface IWorldObjectEvents {
		event ChangePosition PositionChangedEvent;
		event StopExecution StepFinished;
		event CommonEvent EndOfHealthEvent;
	}
}
