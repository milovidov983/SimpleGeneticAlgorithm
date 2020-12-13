namespace AIv2 {
	public class Position {


		public Position(int x, int y) {
			this.X = x;
			this.Y = y;
		}

		public int X { get; }
		public int Y { get; }


		public bool IsValid() 
			=> X < Settings.WORLD_SIZE 
			&& Y < Settings.WORLD_SIZE
			&& X > 0 && Y > 0;
	}
}
