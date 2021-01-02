using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {
	public interface IBotEventObserver {
		void SetDead(Guid id, Position position);
		void SetMove(Guid id, Position from, Position to);
	}

	public class EventSubs {
		private static Dictionary<Guid, EventController> subscriptions = new Dictionary<Guid, EventController>();
		public static EventSubs Instance = new EventSubs();
	


		public void Remove(Guid id) {
			subscriptions.Remove(id);
		}

		public void Add(Guid id, EventController controller) {
			subscriptions.Add(id, controller);
		}

		public EventController this[Guid i] {
			get { return subscriptions[i]; }
			set { subscriptions[i] = value; }
		}
	}


	public class EventController : IWorldObjectEvents {
		public event ChangePosition PositionChangedEvent;
		public event StopExecution StepFinished;
		public event CommonEvent EndOfHealthEvent;

		private EventSubs subscriptions = EventSubs.Instance;

		public void AddSub(Guid id, IBotEventObserver botEventObserver) {
			var controller = new EventController();
			controller.EndOfHealthEvent += botEventObserver.SetDead;
			controller.PositionChangedEvent += botEventObserver.SetMove;

			controller.EndOfHealthEvent += (Guid id, Position position) => {
				var currentController = subscriptions[id];
				currentController.PositionChangedEvent -= botEventObserver.SetMove;
				currentController.EndOfHealthEvent -= botEventObserver.SetDead;
				subscriptions.Remove(id);
			};

			subscriptions[id] = controller;

		}
		
		public void AddSubject(Guid id) {
			subscriptions.Add(id, null);
		}


		public void EndOfHealth(Guid id, Position position) {
			EndOfHealthEvent.Invoke(id, position);
			subscriptions[id].EndOfHealth(id, position);
		}

		public void PositionChanged(Guid id, Position newPosition, Position oldPosition) {
			PositionChangedEvent.Invoke(id, newPosition, oldPosition);
			subscriptions[id].PositionChanged(id, newPosition, oldPosition);
		}

		
	}

	public class StaticEventController : EventController {
		

		public static StaticEventController Instance { get; private set; }

		

		static StaticEventController() {
			Instance = new StaticEventController();
		}
	}
}