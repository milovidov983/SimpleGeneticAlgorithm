using AiLib;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AIv2 {
	public class Context {
		private MapImplementation map;
		private Bot[] bots;
		private bool isRunning = true;

		public Context(MapImplementation map, Bot[] bots) {
			this.map = map;

			this.bots = bots;
	
		}



		public void Run() {

			int botCount = default;
			
			for(int i = 0; isRunning; i++) {
				var b = bots.FirstOrDefault(x=>x.Generation > 5);
				if(b != null) {
					b.EnableLog();
				}
				foreach(var bot in bots) {
					bot.Execute();
					botCount = bots.Count(x => x.IsAlive);
					if(botCount <= Settings.WINNER_MAX_COUNT) {
						isRunning = false;
						break;
					}
				}
				if (i % 10 == 0) {
					map.AddObjects(new WorldObjectFactory(map.food), 8);
					map.AddObjects(new WorldObjectFactory(map.poison), 8);
				}
			}
		}

		public Bot[] GetWinners() {
			return bots.Where(x => x.IsAlive).ToArray();
		}

		public void Stop() {
			isRunning = false;
		}
	}
}