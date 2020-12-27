namespace AiApplication {
    using Otter;
    using Otter.Core;
    using Otter.Graphics;
    using Otter.Graphics.Drawables;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;


    namespace InputInteraction {
        class BotPresenter : Entity {
            private int[] added = Array.Empty<int>();
            private int[] removed = Array.Empty<int>();
            private readonly Random rand = new Random((int)DateTime.UtcNow.Ticks);

            private List<PlayerEntity> all = new List<PlayerEntity>();
			private bool _lock;
			private readonly Scene scene;

			private int[] GetAddedAndClear() { 
               
                    var copy = added.ToArray();
                    added = Array.Empty<int>();
                    return copy;
 
            }

            private int[] GetRemovedAndeClear() {
                
                    var copy = removed.ToArray();
                    removed = Array.Empty<int>();
                    return copy;

            }

            private PlayerEntity[] GetAdded() {
                return GetAddedAndClear().Select(index => all[index]).ToArray();
			}

            private PlayerEntity[] GetRemoved() {
                return GetRemovedAndeClear().Select(
                    index => {
                        var entity = all[index];
                        all.RemoveAt(index);
                        return entity;
                    }
                    ).ToArray();
                
            }

            public BotPresenter(Scene scene) {
				Init();
				this.scene = scene;
			}

            public override void Update() {
                base.Update();

                if (!_lock) {
                    _lock = true;
                    if (scene.Input.KeyDown(Key.Delete)) {
                        if (all.Any()) {
                            removed = new[] { rand.Next(0, all.Count - 1) };
                        }
                    }

                    if (scene.Input.KeyDown(Key.Insert)) {
                        var x = rand.Next(0, 496);
                        var y = rand.Next(0, 496);

                        float h = rand.Next(0, 360);
                        float s = rand.Next(1, 1000) / 1000f;
                        float v = rand.Next(1, 1000) / 1000f;
                        var color = Color.FromHSV(h, s, v, 1);
                        all.Add(new PlayerEntity(x, y, color));
                        added = new[] { all.Count - 1 };
                    }

                    foreach (var entity in GetRemoved()) {
                        scene.Remove(entity);
                    }
                    foreach (var entity in GetAdded()) {
                        scene.Add(entity);
                    }
                    _lock = false;
                }
            }

			private void Init() {
				all = new List<PlayerEntity> {
					new PlayerEntity(32, 32, Color.Blue),
					new PlayerEntity(64, 64, Color.Cyan),
					new PlayerEntity(128, 128, Color.Gold),
					new PlayerEntity(256, 256, Color.Green),
				};
                added = all.Select((_, indes) => indes).ToArray();
			}
		}


        class Program {
            static void Main(string[] args) {
                // Create a Game
                var game = new Game("Input Example", 512, 512);

                // Create a Scene
                var scene = new Scene(512,512);

                var presenter = new BotPresenter(scene);
                // Add a PlayerEntity to the Scene at half of the Game's width, and half of the Game's height (centered)
                scene.Add(presenter);

                // Start the game using the created Scene.
                game.Start(scene);
            }
        }

        class PlayerEntity : Entity {

            /// <summary>
            /// The current move speed of the Entity.
            /// </summary>
            public float MoveSpeed;
			private Image image;

			/// <summary>
			/// The move speed for when the Entity is moving slowly.
			/// </summary>
			public const float MoveSpeedSlow = 5;
            /// <summary>
            /// The move speed for when the Entity is moving quickly.
            /// </summary>
            public const float MoveSpeedFast = 10;

            public PlayerEntity(float x, float y, Color color) : base(x, y) {
				Create();
                Graphic.Color = color;
                // Assign the initial move speed to be the slow speed.
                MoveSpeed = MoveSpeedSlow;
			}

			private void Create() {
				// Create a rectangle image.
				image = Image.CreateRectangle(16);
				// Add the rectangle graphic to the Entity.
				AddGraphic(image);
				// Center the image's origin.
				image.CenterOrigin();
			}

			public override void Update() {
                base.Update();
                // Every update check for input and react accordingly.

                // If the W key is down,
                if (Input.KeyDown(Key.W)) {
                    // Move up by the move speed.
                    Y -= MoveSpeed;
                }
                // If the S key is down,
                if (Input.KeyDown(Key.S)) {
                    // Move down by the move speed.
                    Y += MoveSpeed;
                }
                // If the A key is down,
                if (Input.KeyDown(Key.A)) {
                    // Move left by the move speed.
                    X -= MoveSpeed;
                }
                // If the D key is down,
                if (Input.KeyDown(Key.D)) {
                    // Move right by the move speed.
                    X += MoveSpeed;
                }

            }

        }
    }

}
