﻿using AiLib;
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



		public int Run() {
			int counter = 0;
			int botCount = default;
			
			for(int i = 0; isRunning; i++) {

				foreach(var bot in bots) {
					bot.Execute();
					botCount = bots.Count(x => x.IsAlive);
					if(botCount <= Settings.WINNER_MAX_COUNT) {
						isRunning = false;
						break;
					}
				}
				if (i % Settings.ADD_OBJ_PER_ITERATIONS == 0) {
					map.AddObjects(new WorldObjectFactory(map.food), Settings.ADD_OBJECT_COUNT);
					map.AddObjects(new WorldObjectFactory(map.poison), Settings.ADD_OBJECT_COUNT);
				}
				counter++;
			}
			return counter;
		}

		public Bot[] GetWinners() {
			return bots.Where(x => x.IsAlive).ToArray();
		}

		public void Stop() {
			isRunning = false;
		}
	}
}