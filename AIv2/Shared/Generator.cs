using System;
using System.Collections.Generic;
using System.Linq;

namespace AIv2 {

	public class Generator {
		private int[] _indexes;
		public Generator(int max = Settings.WORLD_SIZE - 1) {
			var rand = new Random((int)DateTime.Now.Ticks);

			_indexes = Enumerable.Range(0, max).ToArray();
			var randomStack = new Stack<int>();
			foreach (var _ in _indexes) {
				for (int i = 0; i < 2; i++) {
					randomStack.Push(rand.Next(0, max));
				}

				var aindex = randomStack.Pop();
				var a = _indexes[aindex];
				var bindex = randomStack.Pop();
				var b = _indexes[bindex];


				_indexes[aindex] = b;
				_indexes[bindex] = a;
			}
		}
		public int cunter = 0;
		public int Get() {
			if(cunter < _indexes.Length) {
				return _indexes[cunter++];
			} else {
				cunter = 0;
				return _indexes[cunter++];
			}
		}
	}
	
}