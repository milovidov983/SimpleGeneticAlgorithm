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
			if(isRunning) {
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
				
				map.AddObjects(map.food, 16);
				map.AddObjects(map.poison, 16);
			}
			//foreach(var bot in bots.Where(x=>x.IsAlive)) {
			//	for (int x = 0; x < Settings.CODE_SIZE; x++) {
			//		Console.Write($"{bot.Brain.Code[x]}\t ");
			//		if((x+1) % 8 == 0) {
			//			Console.Write("\n");
			//		}
			//	}
			//	Console.Write("\n\n");
			//}
			//Logger.Instance.Print();
			//Logger.Instance.logs.Clear();
		}

		public Bot[] GetWinners() {
			return bots.Where(x => x.IsAlive).ToArray();
		}

		public void Stop() {
			isRunning = false;
		}
	}
}