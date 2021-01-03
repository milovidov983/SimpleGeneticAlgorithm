using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class Settings {
		public const int WORLD_SIZE = 64;
		public const int CODE_SIZE = 64;
		public const int MAX_ACTION_COUNT_PER_STEP = 5;
		public const int INIT_HEALTH_COUNT = 32;
		public const int BOT_COUNT = 48;
		public const int BOT_HEALTH_LIMIT = 64;
		public const int WINNER_MAX_COUNT = 8;
		public const int MAX_COMMAND_ID = CODE_SIZE - 1;

		/// <summary>
		/// Кол-во итераций перед добавлением объектов
		/// </summary>
		public const int ADD_OBJ_PER_ITERATIONS = 4;
		public const int ADD_OBJECT_COUNT = 16;

		public const int MUTATION_RATE = 16;
	}

	public static class CommandDirectionIds {
		public const int UP = 0;
		public const int UP_LEFT = 1;
		public const int LEFT = 2;
		public const int LEFT_DOWN = 3;
		public const int DOWN = 4;
		public const int DOWN_RIGHT = 5;
		public const int RIGHT = 6;
		public const int RIGHT_UP = 7;


		public static readonly Dictionary<int, (int x, int y)> IdToCoordinate
			= new Dictionary<int, (int x, int y)> {
				[UP] = (x: 0, y: 1),
				[UP_LEFT] = (x: -1, y: 1),
				[LEFT] = (x: 1, y: 0),
				[LEFT_DOWN] = (x: -1, y: -1),
				[DOWN] = (x: 0, y: -1),
				[DOWN_RIGHT] = (x: 1, y: -1),
				[RIGHT] = (x: 1, y: 0),
				[RIGHT_UP] = (x: 1, y: 1),
			};
	}

	/*
		 * 0..7    сделать шаг
		 * 8..15   схватить еду или нейтрализовать яд
		 * 16..23  посмотреть
		 * 24..31  поворот
		 * 32..62  безусловный переход
	 */
}
