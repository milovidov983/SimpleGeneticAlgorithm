using System;

namespace AiMainLib {
	internal interface IWorld {
		WorldObject GetObjectFrom(Position position);
		void MoveElement(Position from, Position to);
	}

	public class WorldImplementation : IWorld {
		private readonly Wall BorderWall = new Wall();
		private readonly Empty Empty = new Empty();

		public const int WORLD_HEIGHT = 1024;
		public const int WORLD_WIDTH = 1024;


		public WorldObject[,] objects = new WorldObject[WORLD_HEIGHT, WORLD_WIDTH];
		public WorldObject GetObjectFrom(Position position) {
			if (!IsInside(position)) {
				return BorderWall;
			}

			var worldObject = objects[position.X, position.Y];
			ExecuteAdditionalAction(worldObject, position);
			return worldObject;
			
		}

		private void ExecuteAdditionalAction(WorldObject worldObject, Position position) {
			switch (worldObject) {
				case Food _:
				case Poison _:
					objects[position.X, position.Y] = Empty;
					break;
				default:
					break;
			}
		}

		public bool IsInside(Position position) {
			return 0 < position.X && position.X < WORLD_WIDTH
				&& 0 < position.Y && position.Y < WORLD_HEIGHT;
		}

		public void MoveElement(Position from, Position to) {
			objects[to.X,to.Y] = objects[from.X, from.Y];
			objects[from.X, from.Y] = Empty;
		}
	}
}