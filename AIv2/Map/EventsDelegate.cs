using AiLib.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public delegate void ChangePosition(Guid id, Position oldPosition, Position newPosition);
	public delegate void StopExecution();
	public delegate void CommonEvent(Guid id, Position position);
	public delegate void SetGeneration(int gen);
	public delegate void ItemAddedToMap(IItem item);


}
