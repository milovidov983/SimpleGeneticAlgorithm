using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public delegate void ChangePosition(Position oldPosition, Position newPosition);
	public delegate void StopExecution();
	public delegate void CommonEvent(Position position);
	public interface IWorldObjectEvents {
		event ChangePosition PositionChangedEvent;
		event StopExecution StepFinished;
		event CommonEvent EndOfHealthEvent;
	}
}
