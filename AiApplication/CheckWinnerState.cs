using AiApplication.Models;
using AIv2;

namespace AiApplication {
	internal class CheckWinnerState : State {
		private Collection<Bot> bots;

		public CheckWinnerState(Collection<Bot> bots) {
			this.bots = bots;
		}

		public override bool IsRunning => throw new System.NotImplementedException();

		public override void Update() {
			throw new System.NotImplementedException();
		}
	}
}