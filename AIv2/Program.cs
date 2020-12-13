using System;

namespace AIv2 {
	class Program {
		static void Main(string[] args) {
			AiApplication ai = CreateAi();

			ai.Start();

			Console.WriteLine("Hello World!");
		}

		private static AiApplication CreateAi() {
			return new AiApplication();
		}
	}
}
