using AiLib.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public abstract class WorldObject : IItem {
		public Guid Id { get; protected set; } = Guid.NewGuid();
		public Position Position { get; protected set; }

		private CommonEvent DeadEvent;

		private List<IWorldObjectsEventObserver> observers = new List<IWorldObjectsEventObserver>();

		protected WorldObject() {
			DeadEvent += Unsubscribe;
			
		}
		public void AddSubscription(IWorldObjectsEventObserver observer) {
			DeadEvent += observer.SetDead;
			observers.Add(observer);
		}
		private void Unsubscribe(Guid id, Position position) {
			observers.ForEach(x => DeadEvent -= x.SetDead);
			DeadEvent -= Unsubscribe;
		}

		public void SetDead(Position position) {
			DeadEvent?.Invoke(Id, position);
		}

		public void InitPosition(int x, int y) {
			Position = new Position(x, y);
		}
	}
	public class BotObject : WorldObject {
		public BotObject(Guid id, Position position) { Id = id; Position = position; }
		public override string ToString() {
			return $"B";
		}
	}
	public class Wall : WorldObject {
		public int Width = 32;
		public int Height = 4;
		public override string ToString() {
			return $"W";
		}

	}
	public class Food : WorldObject {
		public override string ToString() {
			return $"F";
		}
	}
	public class Poison : WorldObject {
		public override string ToString() {
			return $"P";
		}
	}
	public class Empty : WorldObject {
		public override string ToString() {
			return $".";
		}
	}
}
