using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {
	public class MapImplementation {
		public readonly Empty empty = new Empty();
		public readonly BotObject bot = new BotObject();
		public readonly Food food = new Food();
		public readonly Poison poison = new Poison();
		public readonly Wall wall = new Wall();

		
		public WorldObject[,] map 
			= new WorldObject[Settings.WORLD_SIZE, Settings.WORLD_SIZE];

		public void PrintMap() {
			Console.WriteLine();
			int rows = map.GetUpperBound(0) + 1;
			int columns = map.Length / rows;

			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < columns; j++) {
					Console.Write(map[i, j].ToString());
				}
				Console.WriteLine();
			}
			Console.WriteLine();
		}

		public void CreateAndFillWorldObjects() {
			int rows = map.GetUpperBound(0) + 1;
			int columns = map.Length / rows;

			for (int i = 0; i < rows; i++) {
				for (int j = 0; j < columns; j++) {
					map[i, j] = empty;
				}
			}

			AddObjects(food);
			AddObjects(poison);
			//AddObjects(wall, 128);
			
		}


		public void AddObjects(WorldObject type, int count = Settings.WORLD_SIZE) {
			var generator = new Generator();
			var counter = 0;
			while (counter++ < count) {
				var x = generator.Get();
				var y = generator.Get();
				if(map[x,y] is Empty) {
					map[x, y] = type;
				}
			}
		}

		public void Add(Bot[] bots) {
			var generator = new Generator();
			var botsStack = new Stack<Bot>(bots);
			while (botsStack.Any()) {
				var x = generator.Get();
				var y = generator.Get();

				var currentObject = map[x, y];

				if(currentObject is Empty) {
					map[x, y] = this.bot;
					var bot = botsStack.Pop();
					bot.InitPosition(x, y);
				}
			}
		}

		public void Add(IWorldObjectEvents worldObjectEvents) {
			worldObjectEvents.PositionChangedEvent += MoveElement;
			worldObjectEvents.EndOfHealthEvent += ClearCell;
		}

		public WorldObject Get(Position position) {
			return map[position.X, position.Y];
		}

		public void ClearCell(Position position) {
			map[position.X, position.Y] = empty;
		}

		private void MoveElement(Position from, Position to) {
			map[to.X, to.Y] = map[from.X, from.Y];
			map[from.X, from.Y] = empty;
		}
	}
}