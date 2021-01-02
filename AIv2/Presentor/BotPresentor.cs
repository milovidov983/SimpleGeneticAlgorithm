using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AiLib.Presentor {
	public class BotPresentor : IBotPresentor {
		private Dictionary<Guid, BotView> views = new Dictionary<Guid, BotView>();

		public BotView[] GetViews() {
			return views.Values.ToArray();
		}

		public void SetNextStep() {
			var deleted = views.Values.Where(x => x.State == BotState.Dead).Select(x => x.Id);
			foreach(var id in deleted) {
				views.Remove(id);
			}
		}

		public void InitBots(BotView[] bots) {
			views = bots.ToDictionary(
				x => x.Id,
				x => x
				);
		}
	}
}
