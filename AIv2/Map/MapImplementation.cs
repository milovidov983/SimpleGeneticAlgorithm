using AiLib;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {
	public class MapImplementation : IBotEventObserver {
		public readonly Empty empty = new Empty();

		public readonly Food food = new Food();
		public readonly Poison poison = new Poison();
		public readonly Wall wall = new Wall();

		public ItemAddedToMap AddedToMapEvent;

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

			AddObjects(new WorldObjectFactory(food), 16);
			AddObjects(new WorldObjectFactory(poison), 16);
			//AddObjects(wall, 128);
			
		}

		public void AddObjects(WorldObjectFactory factory, int count = Settings.WORLD_SIZE) {
			var generator = new Generator();
			var counter = 0;
			while (counter++ < count) {
				var x = generator.Get();
				var y = generator.Get();

				var currentObject = map[x, y];

				if (currentObject is Empty) {
					var item = factory.Create();
					item.InitPosition(x, y);
					map[x, y] = item;

					item.AddSubscription(this);

					AddedToMapEvent?.Invoke(item);
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
					var item = botsStack.Pop();
					item.InitPosition(x, y);
					map[x, y] = new BotObject(item.Id, item.Position);

					item.AddSububscriptions(this);

					AddedToMapEvent?.Invoke(item);
				}
			}
		}

		public WorldObject Get(Position position) {
			return map[position.X, position.Y];
		}


		public void SetDead(Guid id, Position position) {
			var curent = map[position.X, position.Y];// = empty;
			if(curent.Id == id) {
				map[position.X, position.Y] = empty;
			}
		}

		public void SetMove(Guid id, Position from, Position to) {
			map[to.X, to.Y] = map[from.X, from.Y];
			SetDead(id, from);
		}

		public void UpGeneration(int gen) {
			//donothing
		}
	}
}