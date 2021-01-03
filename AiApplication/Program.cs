
using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System.Threading.Tasks;

namespace AiApplication {
	public static class Settings {
        public const int WIDTH = 1024;
        public const int HEIGHT = 1024;

        public const int BOT_SIZE = 16;
        /// <summary>
        /// Кол-во итераций перед добавлением объектов
        /// </summary>
        public const int ADD_OBJ_PER_ITERATIONS = 10;
		public const int ADD_OBJECT_COUNT = 8;
	}

	class Program {
        private const int LOOP_COUNT = 2000;
        static async Task Main(string[] args) {
            // Create a Game
            var game = new Game("Input Example", Settings.WIDTH, Settings.HEIGHT);

            // Create a Scene
            var scene = new Scene(Settings.WIDTH, Settings.HEIGHT);

            var grid = new Grid(Settings.WIDTH, Settings.HEIGHT, Settings.BOT_SIZE, Settings.BOT_SIZE, Color.White);

            // Add a PlayerEntity to the Scene at half of the Game's width, and half of the Game's height (centered)
            var botUpdater = new BotUpdater(scene);

            scene.Add(botUpdater);
            scene.AddGraphic(grid);
            // Start the game using the created Scene.
            game.Start(scene);
        }

        
    }
}


