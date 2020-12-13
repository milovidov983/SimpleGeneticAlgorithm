using System;
using System.Collections.Generic;
using System.Text;

namespace AiMainLib {
	interface ICommand {
		void Execute(IUnit unit, int command);
	}
	abstract class ActionController : ICommand {


		public void Execute(IUnit unit, int command) {
			bool isCanDoAction = true;
			while (isCanDoAction) {
				ExecuteImpl(unit, command);
				//isCanDoAction = DetermineActionPosibility(unit, command);
				ChangeCommand(unit);
			}
		}

		

		protected abstract bool ExecuteImpl(IUnit unit, int command);
	}
	class DoStep: ActionController {
		protected override void ExecuteImpl(IUnit unit, int command) {
			Position currentPosition = unit.GetCurrentPosition();

			var (x, y) = Commands.CommandDirections[command];
			var newPosition = new Position(
				x: currentPosition.X + x, 
				y: currentPosition.Y + y);

			unit.SetNewPosition(newPosition);
		}
	}

	class Commands {

		public static readonly Dictionary<int, (int x, int y)> CommandDirections = new Dictionary<int, (int x, int y)> {
			[UP] =         (x:0, y:1),
			[UP_LEFT] =    (x:-1, y:1),
			[LEFT] =       (x:1, y:0),
			[LEFT_DOWN] =  (x:-1, y:-1),
			[DOWN] =       (x:0, y:-1),
			[DOWN_RIGHT] = (x:1, y:-1),
			[RIGHT] =      (x:1, y:0),
			[RIGHT_UP] =   (x:1, y:1),
		};

		/*
		 * 0..8    сделать шаг
		 * 9..16   схватить еду или нейтрализовать яд
		 * 17..24  посмотреть
		 * 25..32  поворот
		 * 33..63  безусловный переход
		 */
		public const int UP = 0;
		public const int UP_LEFT = 1;
		public const int LEFT = 3;
		public const int LEFT_DOWN = 4;
		public const int DOWN = 5;
		public const int DOWN_RIGHT = 6;
		public const int RIGHT = 7;
		public const int RIGHT_UP = 8;


	}
}
