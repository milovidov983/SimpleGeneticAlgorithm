
using AIv2;
using Otter.Core;
using Otter.Graphics;
using Otter.Graphics.Drawables;
using System.Threading.Tasks;

namespace AiApplication {

	class Program {
        static async Task Main(string[] args) {
            var game = new Game("Input Example", ViewSettings.WIDTH, ViewSettings.HEIGHT);
            var scene = new Scene(ViewSettings.WIDTH, ViewSettings.HEIGHT);

            var grid = new Grid(ViewSettings.WIDTH, ViewSettings.HEIGHT, ViewSettings.BOT_SIZE, ViewSettings.BOT_SIZE, Color.White);
            var botUpdater = new BotUpdater(scene);

            scene.Add(botUpdater);
            scene.AddGraphic(grid);

            game.Start(scene);
        }

        
    }
}


