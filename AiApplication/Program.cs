
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

	}

	class Program {

        static async Task Main(string[] args) {
            // Create a Game
            var game = new Game("Input Example", Settings.WIDTH, Settings.HEIGHT);

            // Create a Scene
            var scene = new Scene(Settings.WIDTH, Settings.HEIGHT);

            var grid = new Grid(Settings.WIDTH, Settings.HEIGHT, Settings.BOT_SIZE, Settings.BOT_SIZE, Color.White);
                
            var presenter = new BotUpdater(scene);
			// Add a PlayerEntity to the Scene at half of the Game's width, and half of the Game's height (centered)
			(Bot[] bots, Context context) = Create();
            presenter.Init(bots, context);
            scene.AddGraphic(grid);
            scene.Add(presenter);

            Task.Run(async () => {
                await Task.Delay(1000);
                context.Run();
            
            });
            // Start the game using the created Scene.
            game.Start(scene);
        }



        static (Bot[] bots, Context context) Create() {
            var map = new MapImplementation();
            map.CreateAndFillWorldObjects();
            map.Add(StaticEventController.Instance);

            var commandFactory = new CommandFactory(map);

            var botFactory = new BotFactory(commandFactory);
            var bots = botFactory.CreateBots();
            map.Add(bots);
            var context = new Context(map, bots);
            return (bots, context);
        }
    }
}


