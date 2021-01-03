using AiApplication.Models;
using AIv2;
using Otter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiApplication {


	public abstract class State {
		protected ExecutionContext context;

		public void SetContext(ExecutionContext context) {
			this.context = context;
		}
		public abstract void Update();
		public abstract bool IsRunning { get; }

	}

	public class BotStepState : State {
		public override bool IsRunning => context.IsRunning;
		private Collection<Bot> bots;

		public BotStepState(Bot[] bots) {
			this.bots = new Collection<Bot>(bots);
		}

		public override void Update() {
			var result = bots.GetNext();
			if (result.hasValue) {
				result.value.Execute();
				context.TransitionTo(new CheckWinnerState(bots));
			} else {
				context.TransitionTo(new AddObjectsState());
			}
		}
	}


	public class ExecutionContext  {
		public State _state = null;
		public bool isRunning = true;
		

		public ExecutionContext(State state) {
			TransitionTo(state);
		}

		public void TransitionTo(State state) {
			this._state = state;
			this._state.SetContext(this);
		}

		//public override void Update() {
		//	base.Update();
		//	_state.Update();
		//}

		public bool IsRunning { get => isRunning; }

	}
}
