using SFML.Window;
using SFML.Graphics;
using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {
        static void Main(string[] args)
        {
            //Game of life setup
            uint windowWidth = 800;
            uint windowHeight = 600;

            uint boardHeight = 256;
            uint boardWidth = 256;

            var board = new Board((int)boardWidth, (int)boardHeight);
            var glider = ShapeFactory.MakeGlider();
            var toad = ShapeFactory.MakeToad();
            var blinker = ShapeFactory.MakeBlinker();

            for (int i = 0; i < 100; i++)
            {
                board.RandomInsert(glider);
                board.RandomInsert(blinker);
                board.RandomInsert(toad);
            }

            //SFML setup
            var window = new RenderWindow(new VideoMode(windowWidth, windowHeight),"Game of Life");
            var renderer = new SFML_Renderer(window,boardWidth,boardHeight);

            window.Closed += new EventHandler(OnClosed);
            window.KeyPressed += new EventHandler<KeyEventArgs>(OnKeyPressed);

            //Setup view
            var tileWidth = windowWidth / (float)boardWidth;
            var tileHeight = windowHeight / (float)boardHeight;
            renderer.QuadWidth = tileWidth;
            renderer.QuadHeight = tileHeight;

            while (window.IsOpen)
            {
                window.DispatchEvents();

                //Input detection
                if(Keyboard.IsKeyPressed(Keyboard.Key.Num1))
                {
                    board.RandomInsert(glider);
                }

                else if(Keyboard.IsKeyPressed(Keyboard.Key.Num2))
                {
                    board.RandomInsert(toad);
                }

                else if(Keyboard.IsKeyPressed(Keyboard.Key.Num3))
                {
                    board.RandomInsert(blinker);
                }

                board.UpdateCells();

                Color color = new Color(50, 100, 133, 255);
                window.Clear(Color.Black);

                renderer.DrawBoard(board.Data);

                window.Display();
                Thread.Sleep(100);
            }
        }

        static void OnClosed(object sender, EventArgs e)
        {
            Window window = (Window)sender;
            window.Close();
        }

        static void OnKeyPressed(object sender, KeyEventArgs e)
        {
            Window window = (Window)sender;
            if (e.Code == Keyboard.Key.Escape)
                window.Close();
        }
    }
}
