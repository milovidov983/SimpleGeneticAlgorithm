using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {
	public interface IBotEventObserver: IWorldObjectsEventObserver {
		void SetMove(Guid id, Position from, Position to);
		void UpGeneration(int gen);
	}

	public interface IWorldObjectsEventObserver {
		void SetDead(Guid id, Position position);
	}
}