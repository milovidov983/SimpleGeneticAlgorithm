//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using System.Drawing;
//namespace Live1 {
//	public partial class Form1 : Form {
//		public Form1() {
//			InitializeComponent();
//		}

//		private void Form1_Load(object sender, EventArgs e) {

//		}

//		private void pictureBox1_Click(object sender, EventArgs e) {


//			//SolidBrush myCorp = new SolidBrush(Color.DarkMagenta);
//			//SolidBrush myTrum = new SolidBrush(Color.DarkOrchid);
//			//SolidBrush myTrub = new SolidBrush(Color.DeepPink);
//			//SolidBrush mySeа = new SolidBrush(Color.Blue);
//			////Выбираем перо myPen желтого цвета толщиной в 2 пикселя:
//			//Pen myWind = new Pen(Color.Yellow, 2);
//			//// Закрашиваем фигуры
//			//g.FillRectangle(myTrub, 300, 125, 75, 75); // 1 труба (прямоугольник)
//			//g.FillRectangle(myTrub, 480, 125, 75, 75); // 2 труба (прямоугольник)
//			//g.FillPolygon(myCorp, new Point[]      // корпус (трапеция)
//			//  {
//			//	new Point(100,300),new Point(700,300),
//			//	new Point(700,300),new Point(600,400),
//			//	new Point(600,400),new Point(200,400),
//			//	new Point(200,400),new Point(100,300)
//			//  }
//			//);
//			//g.FillRectangle(myTrum, 250, 200, 350, 100); // палуба (прямоугольник)
//			//											 // Море - 12 секторов-полуокружностей
//			//int x = 50;
//			//int Radius = 50;
//			//while (x <= pictureBox1.Width - Radius) {
//			//	g.FillPie(mySeа, 0 + x, 375, 50, 50, 0, -180);
//			//	x += 50;
//			//}
//			//// Иллюминаторы 
//			//for (int y = 300; y <= 550; y += 50) {
//			//	g.DrawEllipse(myWind, y, 240, 20, 20); // 6 окружностей
//			//}

//			// Create pen.



//			Run().GetAwaiter().GetResult();


//			//Draw lines to screen.

//		}

//		private async Task Run() {
//			Graphics g = pictureBox1.CreateGraphics();
//			DrawMap(g, Y_MAX, X_MAX);

//			SolidBrush myTrub = new SolidBrush(Color.DeepPink);
//			for(int i = 0; i < X_MAX; i += 20) {
//				g.FillRectangle(myTrub, i, 0, 20, 20);
//				await Task.Delay(500);
//				DrawMap(g, Y_MAX, X_MAX);
//			}
//			//g.FillRectangle(myTrub, 20, 20, 20, 20);



//		}

//		const int Y_MAX = 380;
//		const int X_MAX = 780;
//		private static void DrawMap(Graphics g, int Y_MAX, int X_MAX) {
//			Pen pen = new Pen(Color.Gray, 1);
//			g.Clear(Color.White);
//			for (int y = 0; y < 800; y += 20) {
//				var p1 = new Point(y, 0);
//				var p2 = new Point(y, Y_MAX);
//				g.DrawLine(pen, p1, p2);
//			}

//			for (int x = 0; x < 400; x += 20) {
//				var p1 = new Point(0, x);
//				var p2 = new Point(X_MAX, x);
//				g.DrawLine(pen, p1, p2);
//			}
//		}
//	}
//}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Live1 {
    public partial class Form1 : Form {
        // check every x frames, calculate fps, adjust x
        private readonly FrameInfo _frameInfo = new FrameInfo(DateTime.Now);
        private readonly List<Animation> _animations = new List<Animation>();

        public Form1() {
            InitializeComponent();

            this.DoubleBuffered = true;

            FrameInfoVisible = false; // Set to true if needed. 
            RegisterAnimation(new Snake(this));
        }

        public bool FrameInfoVisible { get; set; }

        public void RegisterAnimation(Animation animation) {
            _animations.Add(animation);
        }

        private int _skipframes = 1;

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            // Skip updating if needed.
            if (_skipframes > 0) {
                _skipframes--;
            } else {
                _frameInfo.Update();
                foreach (var animation in _animations)
                    animation.UpdatePositions(_frameInfo);

                _skipframes = 0; // adjust to skip frames or leave at 0 to not skip frames. 
            }

            // init drawing
            var gfx = pictureBox1.CreateGraphics();
            gfx.Clear(Color.Black);

            // draw each frame. 
            foreach (var animation in _animations) {
                animation.Draw(gfx);
                gfx.ResetTransform(); // in case the animation used transform methods.
            }

            // draw timer info on top
            if (FrameInfoVisible)
                _frameInfo.Draw(gfx);

            // wait till a certain time has passed before drawing again. 
            //Thread.Sleep(10);
            Invalidate(); // ensure new paint soon
        }
    }

    /// <summary>
    /// abstract base class for animations.
    /// An animation contains two methods. First one used for updating animation data, eg positions.
    /// Second one used for drawing the data onto a graphics object.
    /// </summary>
    public abstract class Animation : IDrawable, IAnimatable {
        public abstract void Draw(Graphics gfx);

        public abstract void UpdatePositions(FrameInfo frameInfo);
    }

    /// <summary>
    /// Contains info about our frames
    /// </summary>
    public class FrameInfo {
        public DateTime FirstFrameTime { get; set; }
        public DateTime PrevFrameTime { get; set; }
        public DateTime FrameTime { get; set; }
        public int FrameCount { get; set; }
        public double FramesPerSecond { get; set; }

        public FrameInfo(DateTime now) {
            FirstFrameTime = now;
        }

        public void Update() {
            PrevFrameTime = FrameTime;
            FrameTime = DateTime.Now;
            FrameCount++;
            FramesPerSecond = 1000.0 / (FrameTime - PrevFrameTime).TotalMilliseconds;
        }

        public void Draw(Graphics gfx) {
            gfx.DrawString("Frame time", SystemFonts.DefaultFont, Brushes.Black, 0, 0);
            gfx.DrawString(String.Format(": {0:hh:mm:ss.zzzz}", FrameTime - FirstFrameTime), SystemFonts.DefaultFont, Brushes.Black, 70, 0);
            gfx.DrawString("Frame", SystemFonts.DefaultFont, Brushes.Black, 0, 16);
            gfx.DrawString(": " + FrameCount, SystemFonts.DefaultFont, Brushes.Black, 70, 16);
            gfx.DrawString("FPS", SystemFonts.DefaultFont, Brushes.Black, 0, 32);
            gfx.DrawString(": " + FramesPerSecond, SystemFonts.DefaultFont, Brushes.Black, 70, 32);
        }
    }

    internal interface IAnimatable {
        void UpdatePositions(FrameInfo frameInfo);
    }

    internal interface IDrawable {
        void Draw(Graphics gfx);
    }

    /// <summary>
    /// Animation module
    /// </summary>
    internal class Snake : Animation {
        public Snake(Control form) {
            form.KeyDown += form_KeyDown;
            form.KeyUp += form_KeyUp;
        }

        readonly Dictionary<Keys, bool> _keyMap = new Dictionary<Keys, bool>
        {
            { Keys.Up, false },
            { Keys.Down, false },
            { Keys.Left, false },
            { Keys.Right, false }
        };

        void form_KeyUp(object sender, KeyEventArgs e) {
            if (_keyMap.ContainsKey(e.KeyCode))
                _keyMap[e.KeyCode] = false;
        }
        void form_KeyDown(object sender, KeyEventArgs e) {
            if (_keyMap.ContainsKey(e.KeyCode))
                _keyMap[e.KeyCode] = true;
        }

        private PointF _headPos = new PointF(100.0f, 100.0f);

        public override void UpdatePositions(FrameInfo info) {
            // Ensure that the motion is moving at a 
            var speed = 100; // 100 units within 1 second
            var perc = (double)(info.FrameTime - info.PrevFrameTime).TotalMilliseconds / 1000;
            var displaceAmount = (float)(speed * perc);

            if (_keyMap[Keys.Up])
                _headPos.Y -= displaceAmount;
            if (_keyMap[Keys.Down])
                _headPos.Y += displaceAmount;
            if (_keyMap[Keys.Right])
                _headPos.X += displaceAmount;
            if (_keyMap[Keys.Left])
                _headPos.X -= displaceAmount;
        }

        public override void Draw(Graphics gfx) {
            gfx.SmoothingMode = SmoothingMode.AntiAlias;
            gfx.DrawString(String.Format("[{0},{1}]", _headPos.X, _headPos.Y), SystemFonts.DefaultFont, Brushes.White, 0.0f, 48.0f);
            gfx.DrawEllipse(Pens.White, _headPos.X, _headPos.Y, 10.0f, 10.0f);
        }
    }

}


