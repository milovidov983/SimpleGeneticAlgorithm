using System;
using System.Collections.Generic;
using System.Text;

namespace AIv2 {
	public class Logger {
		public Logger() {
            Console.Clear();
            origRow = Console.CursorTop;
            origCol = Console.CursorLeft;

        }
        protected int origRow;
        protected int origCol;

        public void WriteAt(string s, int x, int y) {
            try {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            } catch (ArgumentOutOfRangeException e) {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
    }
}
